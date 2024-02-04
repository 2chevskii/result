# `ResultConversionExtensions` class

```cs
public static class ResultConversionExtensions {...}
```

## Static methods

### AsOk&lt;T, E&gt;(T)

```cs
public static Result<T, E> AsOk<T, E>(this T self);
```

### AsOk&lt;T&gt;(T)

```cs
public static Result<T, Exception> AsOk<T>(this T self);
```

### AsErr&lt;T, E&gt;(E)

```cs
public static Result<T, E> AsErr<T, E>(this E self);
```

### AsErr&lt;T&gt;(Exception)

```cs
public static Result<T, Exception> AsErr<T>(this Exception self);
```
