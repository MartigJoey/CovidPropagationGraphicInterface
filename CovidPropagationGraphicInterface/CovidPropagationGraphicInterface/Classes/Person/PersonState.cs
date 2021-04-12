using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidPropagationGraphicInterface.Classes.Person
{
    enum PersonState
    {
        Healthy,
        Infected,
        Asymptomatic
    }

    class State
    {
        private PersonState _state;

        public PersonState CurrentState { get => _state; set => _state = value; }
        public State()
        {
            CurrentState = PersonState.Healthy;
        }
    }
}
