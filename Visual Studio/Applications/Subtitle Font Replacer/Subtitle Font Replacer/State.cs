using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SubtitleFontReplacer.Annotations;

namespace SubtitleFontReplacer
{
    public class State : INotifyPropertyChanged
    {
        private readonly Stack<string> states = new Stack<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        private class StateHolder : IDisposable
        {
            private readonly State state;

            public StateHolder(State state, string newState)
            {
                this.state = state;

                this.state.PushState(newState);
            }

            public void Dispose()
            {
                state.PopState();
            }
        }

        public State(string initialState)
        {
            states.Push(initialState);
        }

        public string Current => states.Peek();

        public IDisposable OnState(string state)
        {
            return new StateHolder(this, state);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PushState(string state)
        {
            states.Push(state);

            OnPropertyChanged(nameof(Current));
        }

        private void PopState()
        {
            states.Pop();

            OnPropertyChanged(nameof(Current));
        }
    }
}
