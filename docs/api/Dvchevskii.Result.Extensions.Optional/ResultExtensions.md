# `ResultExtensions` class

```cs
public static class ResultExtensions {...}
```

## Static methods

### Ok&lt;T, E&gt;(Result&lt;T, E&gt;)

```cs
public static Option<T> Ok<T, E>(this Result<T, E> self);
```

### Err&lt;T, E&gt;(Result&lt;T, E&gt;)

```cs
public static Option<E> Err<T, E>(this Result<T, E> self);
```

### Transpose&lt;T, E&gt;(Result&lt;Option&lt;T&gt;, E&gt;)

```cs
public static Option<Result<T, E>> Transpose<T, E>(this Result<Option<T>, E> self);
```
