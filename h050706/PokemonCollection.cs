using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace h050706.EnumPokemon
{
    public class PokemonCollection : IEnumerable<Pokemon>
    {
        private Pokemon[] _pokemons;
        private int _count;

        public PokemonCollection()
        {
            _pokemons = new Pokemon[1];
            _count = 0;
        }

        public void Add(Pokemon pokemon)
        {
            if (_count == _pokemons.Length)
            {
                EnsureCapacity();
            }
            _pokemons[_count++] = pokemon;
        }

        public IEnumerator<Pokemon> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void EnsureCapacity()
        {
            var newPokemons = new Pokemon[_pokemons.Length * 2];
            Array.Copy(_pokemons, newPokemons, _count);
            _pokemons = newPokemons;
        }

        private sealed class Enumerator : IEnumerator<Pokemon>
        {
            private PokemonCollection _pokemonCollection;
            private int _index;
            public Pokemon Current => _pokemonCollection._pokemons[_index];

            public Enumerator(PokemonCollection pokemonCollection)
            {
                _pokemonCollection = pokemonCollection;
                _index = -1;
            }

            public bool MoveNext()
            {
                _index++;
                return _index < _pokemonCollection._count;
            }

            public void Reset()
            {
                _index = -1;
            }

            public void Dispose()
            {
            }

            object IEnumerator.Current => Current;
        }
    }
}
