# `Result` class

```cs
public abstract class Result {...}
```

## Static methods

### Ok&lt;T, E&gt;(T)

```cs
public static Result<T, E> Ok<T, E>(T value);
```

### Ok&lt;T&gt;(T)

```cs
public static Result<T, Exception> Ok<T>(T value);
```

### Err&lt;T, E&gt;(E)

```cs
public static Result<T, E> Err<T, E>(E error);
```

### Err&lt;T&gt;(Exception)

```cs
public static Result<T, Exception> Err<T>(Exception error);
```

## Instance methods

### State()

```cs
public abstract ResultState State();
```

### IsOk()

```cs
public bool IsOk()
```

### IsErr()

```cs
public bool IsErr();
```

### Is(ResultState)

```cs
public bool Is(ResultState state);
```

### NumericState()

```cs
public byte NumericState();
```

### CompareTo(ResultState)

```cs
public int CompareTo(ResultState other);
```

### Equals(ResultState)

```cs
public bool Equals(ResultState state);
```
