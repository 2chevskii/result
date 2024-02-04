export default [
  {
    type: "namespace",
    name: "Dvchevskii.Result",
    members: [
      {
        type: "class",
        name: "Result",
        members: [
          "Ok<T, E>(T)",
          "Ok<T>(T)",
          "Err<T, E>(E)",
          "Err<T>(Exception)",
          "State()",
          "IsOk()",
          "IsErr()",
          "Is(ResultState)",
          "NumericState()",
          "CompareTo(ResultState)",
          "Equals(ResultState)",
        ],
      },
      {
        type: "class",
        name: "Result&lt;T, E&gt;",
        uriName: "Result_2",
        members: [
          "GetUnderlyingOkType()",
          "GetUnderlyingErrType()",
          "IsOkAnd(Predicate<T>)",
          "IsErrAnd(Predicate<E>)",
          "And<U>(Result<U, E>)",
          "AndThen<U>(Func<T, Result<U, E>>)",
          'Or<F>(Result<T, F>)',
          'OrElse<F>(Func<E, Result<T, F>>)',
          'CompareTo(Result<T, E> other)',
          'Equals(object)',
          'Equals(Result<T, E> other)',
          'GetHashCode()',
          'Inspect(Action<T>)',
          'InspectErr(Action<E>)',
          'Map<U>(Func<T, U>)',
          'MapOr<U>(Func<T, U>, U)',
          'MapOrElse<U>(Func<T, U>, Func<E, U>)',
          'MapErr<F>(Func<E, F>)',
          'Expect(string)',
          'Unwrap()',
          'UnwrapOrDefault()',
          'UnwrapOr(T defaultValue)',
          'UnwrapOrElse(Func<E, T> defaultValueFactory)',
          'UnwrapUnchecked()',
          'UnwrapErrUnchecked()',
          'ExpectErr(string)',
          'UnwrapErr()'
        ],
      },
      {
        type: "class",
        name: "ResultState",
        members: ["Err", "Ok"],
      },
      {
        type: "interface",
        name: "IOk",
      },
      {
        type: "interface",
        name: "IOk&lt;T&gt;",
        uriName: "IOk_1",
        members: ["Value"],
      },
      {
        type: "interface",
        name: "IErr",
      },
      {
        type: "interface",
        name: "IErr&lt;E&gt;",
        uriName: "IErr_1",
        members: ["Error"],
      },
    ],
  },
  {
    type: "namespace",
    name: "Dvchevskii.Result.Extensions",
    members: [
      {
        type: "class",
        name: "ResultExtensions",
        members: ["Match<T, E, U>(Result<T, E>, Func<T, U>, Func<E, U>)"],
      },
      {
        type: "class",
        name: "ResultConversionExtensions",
        members: [
          'AsOk<T, E>(T)',
          'AsOk<T>(T)',
          'AsErr<T, E>(E)',
          'AsErr<T>(Exception)'
        ],
      },
    ],
  },
  {
    type: "namespace",
    name: "Dvchevskii.Result.Exceptions",
    members: [
      {
        type: "class",
        name: "UnexpectedResultException",
      },
    ],
  },
  {
    type: 'namespace',
    name: 'Dvchevskii.Result.Extensions.Optional',
    members: [
      {
        type: 'class',
        name: 'ResultExtensions',
        members: [
          'Ok<T, E>(Result<T, E>)',
          'Err<T, E>(Result<T, E>)',
          'Transpose<T, E>(Result<Option<T>, E>)',
        ]
      }
    ]
  }
];
