# `Result<T, E>` class

```cs
public abstract class Result<T, E> {...}
```

## Instance methods

### GetUnderlyingOkType()

```cs
public Type GetUnderlyingOkType();
```

### GetUnderlyingErrType()

```cs
public Type GetUnderlyingErrType();
```

### IsOkAnd(Predicate&lt;T&gt;)

```cs
public bool IsOkAnd(Predicate<T> predicate);
```

### IsErrAnd(Predicate&lt;E&gt;)

```cs
public bool IsErrAnd(Predicate<E> predicate);
```

### And&lt;U&gt;(Result&lt;U, E&gt;)

```cs
public Result<U, E> And<U>(Result<U, E> result);
```

### AndThen&lt;U&gt;(Func&lt;T, Result&lt;U, E&gt;&gt;)

```cs
public Result<U, E> AndThen<U>(Func<T, Result<U, E>> factory);
```

### Or&lt;F&gt;(Result&lt;T, F&gt;)

```cs
public Result<T, F> Or<F>(Result<T, F> result);
```

### OrElse&lt;F&gt;(Func&lt;E, Result&lt;T, F&gt;&gt;)

```cs
public Result<T, F> OrElse<F>(Func<E, Result<T, F>> factory);
```

### CompareTo(Result&lt;T, E&gt; other)

```cs
public int CompareTo(Result<T, E> other);
```

### Equals(object)

```cs
public override bool Equals(object obj);
```

### Equals(Result&lt;T, E&gt; other)

```cs
public bool Equals(Result<T, E> other);
```

### GetHashCode()

```cs
public override int GetHashCode();
```

### Inspect(Action&lt;T&gt;)

```cs
public Result<T, E> Inspect(Action<T> inspector);
```

### InspectErr(Action&lt;E&gt;)

```cs
public Result<T, E> InspectErr(Action<E> inspector);
```

### Map&lt;U&gt;(Func&lt;T, U&gt;)

```cs
public Result<U, E> Map<U>(Func<T, U> mapper);
```

### MapOr&lt;U&gt;(Func&lt;T, U&gt;, U)

```cs
public U MapOr<U>(Func<T, U> mapper, U defaultValue);
```

### MapOrElse&lt;U&gt;(Func&lt;T, U&gt;, Func&lt;E, U&gt;)

```cs
public U MapOrElse<U>(Func<T, U> mapper, Func<E, U> elseMapper);
```

### MapErr&lt;F&gt;(Func&lt;E, F&gt;)

```cs
public Result<T, F> MapErr<F>(Func<E, F> errMapper);
```

### Expect(string)

```cs
public T Expect(string message)
```

### Unwrap()

```cs
public T Unwrap()
```

### UnwrapOrDefault()

```cs
public T UnwrapOrDefault()
```

### UnwrapOr(T defaultValue)

```cs
public T UnwrapOr(T defaultValue)
```

### UnwrapOrElse(Func&lt;E, T&gt; defaultValueFactory)

```cs
public T UnwrapOrElse(Func<E, T> defaultValueFactory)
```

### UnwrapUnchecked()

```cs
public abstract T UnwrapUnchecked();
```

### UnwrapErrUnchecked()

```cs
public abstract E UnwrapErrUnchecked();
```

### ExpectErr(string)

```cs
public E ExpectErr(string message)
```

### UnwrapErr()

```cs
public E UnwrapErr()
```
