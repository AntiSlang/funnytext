using System;

namespace funnytext.Services
{
    public class RadioGroup
    {
        private readonly Func<bool>[] _getters;
        private readonly Action<bool>[] _setters;

        public RadioGroup(Func<bool>[] getters, Action<bool>[] setters)
        {
            _getters = getters;
            _setters = setters;
        }

        public void Set(int which)
        {
            for (int i = 0; i < _setters.Length; i++)
            {
                _setters[i](i == which);
            }
        }

        public int Get()
        {
            for (int i = 0; i < _getters.Length; i++)
            {
                if (_getters[i]())
                {
                    return i;
                }
            }
            return 0;
        }
    }
}