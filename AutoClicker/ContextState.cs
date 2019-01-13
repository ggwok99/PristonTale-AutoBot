using AutoClicker.Controls;
using AutoClicker.Enums;
using AutoClicker.Interfaces;
using AutoClicker.Strategies;
using System;
using System.Collections.Generic;

namespace AutoClicker
{
    public abstract class ContextState
    {
        protected IntPtr _hWnd;

        protected List<IObserver> _observers = new List<IObserver>();
        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }
        public void Attach(List<IObserver> observers)
        {
            _observers.AddRange(observers);
        }
        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }
        public void Notify()
        {
            foreach (IObserver o in _observers)
            {
                o.SubjectUpdated();
            }
        }

        public MainStreamState MainStreamState { get; set; }

        public abstract void SwitchState(UControlContext context);
        public abstract void Execute(dynamic potData, dynamic buffData, dynamic atkData, dynamic refillPotData, dynamic pickItemData);


        protected PottingStrategy _pottingStrategy;
        public void SetPottingStrategy(PottingStrategy pottingStrategy)
        {
            Detach(_pottingStrategy);
            _pottingStrategy = pottingStrategy;
            _pottingStrategy.SetSubject(this);
            AddMainStreamEventHandlers(_pottingStrategy);
            Attach(_pottingStrategy);
        }

        protected BuffStrategy _buffStrategy;
        public void SetBuffStrategy(BuffStrategy buffStrategy)
        {
            Detach(_buffStrategy);
            _buffStrategy = buffStrategy;
            _buffStrategy.SetSubject(this);
            AddMainStreamEventHandlers(_buffStrategy);
            Attach(_buffStrategy);
        }

        protected AttackingStrategy _attackingStrategy;
        public void SetAttackingStrategy(AttackingStrategy attackingStrategy)
        {
            Detach(_attackingStrategy);
            _attackingStrategy = attackingStrategy;
            _attackingStrategy.SetSubject(this);
            AddMainStreamEventHandlers(_attackingStrategy);
            Attach(_attackingStrategy);
        }

        protected RefillPotStrategy _refillPotStrategy;
        public void SetRefillPotStrategy(RefillPotStrategy refillPotStrategy)
        {
            Detach(_refillPotStrategy);
            _refillPotStrategy = refillPotStrategy;
            _refillPotStrategy.SetSubject(this);
            AddMainStreamEventHandlers(_refillPotStrategy);
            Attach(_refillPotStrategy);
        }

        protected PickItemStrategy _pickItemStrategy;
        public void SetPickItemStrategy(PickItemStrategy pickItemStrategy)
        {
            Detach(_pickItemStrategy);
            _pickItemStrategy = pickItemStrategy;
            _pickItemStrategy.SetSubject(this);
            AddMainStreamEventHandlers(_pickItemStrategy);
            Attach(_pickItemStrategy);
        }

        private void AddMainStreamEventHandlers(AutoStrategy strategy)
        {
            strategy.RequestMainStream += (s, e) =>
            {
                if (MainStreamState != MainStreamState.Attack)
                {
                    return;
                }

                MainStreamState = strategy.ThisState();
                Notify();
            };

            strategy.ReleaseMainStream += (s, e) =>
            {
                if (MainStreamState == MainStreamState.Attack)
                {
                    MainStreamState = MainStreamState.PickItem;
                }

                MainStreamState = MainStreamState.Attack;
                Notify();
            };
        }

        protected void LoadData(dynamic potData, dynamic buffData, dynamic atkData, dynamic refillPotData, dynamic pickItemData)
        {
            _pottingStrategy.LoadData(potData);
            _buffStrategy.LoadData(buffData);
            _attackingStrategy.LoadData(atkData);
            _refillPotStrategy.LoadData(refillPotData);
            _pickItemStrategy.LoadData(pickItemData);
        }

        public void InitIntPtr(IntPtr intPtr)
        {
            _pottingStrategy.InitIntPtr(intPtr);
            _buffStrategy.InitIntPtr(intPtr);
            _attackingStrategy.InitIntPtr(intPtr);
            _refillPotStrategy.InitIntPtr(intPtr);
            _pickItemStrategy.InitIntPtr(intPtr);
        }
    }

    public class ContextRunningState : ContextState
    {
        public ContextRunningState(PottingStrategy pottingStrategy, BuffStrategy buffStrategy, AttackingStrategy attackingStrategy, RefillPotStrategy refillPotStrategy, PickItemStrategy pickItemStrategy)
        {
            _pottingStrategy = pottingStrategy;
            _buffStrategy = buffStrategy;
            _attackingStrategy = attackingStrategy;
            _refillPotStrategy = refillPotStrategy;
            _pickItemStrategy = pickItemStrategy;
        }

        public override void Execute(dynamic potData, dynamic buffData, dynamic atkData, dynamic refillPotData, dynamic pickItemData)
        {
            LoadData(potData, buffData, atkData, refillPotData, pickItemData);
            _pottingStrategy.Start();
            _buffStrategy.Start();
            _attackingStrategy.Start();
            if (refillPotData != null)
            {
                _refillPotStrategy.Start();
            }
            _pickItemStrategy.Start();
        }

        public override void SwitchState(UControlContext context)
        {
            context.State = new ContextStoppedState(_pottingStrategy, _buffStrategy, _attackingStrategy, _refillPotStrategy, _pickItemStrategy);
            context.State.Attach(_observers);
            context.State.Notify();
        }

        public override string ToString()
        {
            return "Running";
        }
    }

    public class ContextStoppedState : ContextState
    {
        public ContextStoppedState(IntPtr intPtr)
        {
            SetPottingStrategy(new DeactivePottingStrategy(intPtr));
            SetBuffStrategy(new DeactiveBuffStrategy(intPtr));
            SetAttackingStrategy(new DeactiveAttackingStrategy(intPtr));
            SetRefillPotStrategy(new DeactiveRefillPotStrategy(intPtr));
            SetPickItemStrategy(new DeactivePickItemStrategy(intPtr));
        }

        public ContextStoppedState(PottingStrategy pottingStrategy, BuffStrategy buffStrategy, AttackingStrategy attackingStrategy, RefillPotStrategy refillPotStrategy, PickItemStrategy pickItemStrategy)
        {
            _pottingStrategy = pottingStrategy;
            _buffStrategy = buffStrategy;
            _attackingStrategy = attackingStrategy;
            _refillPotStrategy = refillPotStrategy;
            _pickItemStrategy = pickItemStrategy;
        }

        public override void Execute(dynamic potData, dynamic buffData, dynamic atkData, dynamic refillPotData, dynamic pickItemData)
        {
            LoadData(potData, buffData, atkData, refillPotData, pickItemData);
            _pottingStrategy.Stop();
            _buffStrategy.Stop();
            _attackingStrategy.Stop();
            _refillPotStrategy.Stop();
            _pickItemStrategy.Stop();
        }

        public override void SwitchState(UControlContext context)
        {
            context.State = new ContextRunningState(_pottingStrategy, _buffStrategy, _attackingStrategy, _refillPotStrategy, _pickItemStrategy);
            context.State.Attach(_observers);
            context.State.Notify();
        }

        public override string ToString()
        {
            return "Stopped";
        }
    }
}
