# `ResultExtensions` class

```cs
public static class ResultExtensions {...}
```

## Static methods

### Match&lt;T, E, U&gt;(Result&lt;T, E&gt;, Func&lt;T, U&gt;, Func&lt;E, U&gt;)

```cs
public static U Match<T, E, U>(this Result<T, E> result, Func<T, U> mapOk, Func<E, U> mapErr);
```
