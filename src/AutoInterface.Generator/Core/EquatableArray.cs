using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AutoInterface.Generator.Core;

public readonly struct EquatableArray<T> : IReadOnlyList<T>, IEquatable<EquatableArray<T>>
{
    private readonly T[] _array;

    public EquatableArray(IEnumerable<T> elements)
    {
        _array = elements.ToArray();
    }

    public bool Equals(EquatableArray<T> other)
    {
        return _array != null && _array.SequenceEqual(other._array);
    }

    public override bool Equals(object? obj)
    {
        return obj is EquatableArray<T> other && Equals(other);
    }

    public override int GetHashCode()
    {
        if (_array == null) return 0;

        unchecked
        {
            var hash = 17;
            foreach (var item in _array)
            {
                hash = hash * 31 + (item != null ? item.GetHashCode() : 0);
            }

            return hash;
        }
    }

    public static bool operator ==(EquatableArray<T> left, EquatableArray<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(EquatableArray<T> left, EquatableArray<T> right)
    {
        return !(left == right);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return (_array ?? []).AsEnumerable().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count => _array.Length;

    public T this[int index] => _array[index];
}

public static class EquatableArrayExtensions
{
    public static EquatableArray<T> ToEquatableArray<T>(this IEnumerable<T> array)
        => new(array);
}