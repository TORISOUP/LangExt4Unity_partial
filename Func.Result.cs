
using System;
using System.ComponentModel;

namespace LangExt
{
    /// <summary>
    /// 関数がnullを返したことを表す例外です。
    /// この例外は、Func.ToResultの結果としてのみ使用されます。
    /// </summary>
    public sealed class NullResultException : Exception
    {
        /// <summary>objがNullResultExceptionであればtrueを返します。</summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) { return obj is NullResultException; }
        /// <summary>このオブジェクトのハッシュコードを返します。</summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() { return 11; }
        /// <summary>このオブジェクトの文字列表現を返します。</summary>
        public override string ToString() { return "NullResultException"; }

        /// <summary>
        /// 使用しません。
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static new bool Equals(object a, object b)
        {
            return object.Equals(a, b);
        }

        /// <summary>
        /// 使用しません。
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static new bool ReferenceEquals(object a, object b)
        {
            return object.ReferenceEquals(a, b);
        }
    }

    partial class Func
    {
        /// <summary>関数の結果をResultで包む関数に変換します。</summary>
        public static Func<Result<T, Exception>> ExnToResultFunc<T>(this Func<T> self)
        {
            return () => { try { return Result.Success(self()); } catch (Exception e) { return Result.Failure(e); } };
        }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullの場合、Failureになります。</summary>
        public static Func<Result<T, Unit>> NullToResultFunc<T>(this Func<T> self) where T : class { return () => Result.Create(self()); }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullの場合、Failureになります。</summary>
        public static Func<Result<T, Unit>> NullToResultFunc<T>(this Func<T?> self) where T : struct { return () => Result.Create(self()); }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullか例外の場合、Failureになります。nullの場合はNullResultExceptionがFailureの値として使用されます。</summary>
        public static Func<Result<T, Exception>> ToResultFunc<T>(this Func<T> self)
        {
            return () =>
            {
                try
                {
                    var res = self();
                    if (res == null)
                        return Result.Failure((Exception)new NullResultException());
                    return Result.Success(res);
                } catch (Exception e) { return Result.Failure(e); }
            };
        }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullか例外の場合、Failureになります。nullの場合はNullResultExceptionがFailureの値として使用されます。</summary>
        public static Func<Result<T, Exception>> ToResultFunc<T>(this Func<T?> self)
            where T : struct
        {
            return () =>
            {
                try
                {
                    var res = self();
                    if (res == null)
                        return Result.Failure((Exception)new NullResultException());
                    return Result.Success(res.Value);
                } catch (Exception e) { return Result.Failure(e); }
            };
        }

        /// <summary>関数の結果をResultで包む関数に変換します。</summary>
        public static Func<T1, Result<U, Exception>> ExnToResultFunc<T1, U>(this Func<T1, U> self)
        {
            return (t1) => { try { return Result.Success(self(t1)); } catch (Exception e) { return Result.Failure(e); } };
        }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullの場合、Failureになります。</summary>
        public static Func<T1, Result<U, Unit>> NullToResultFunc<T1, U>(this Func<T1, U> self)
            where U : class
        {
            return (t1) => Result.Create(self(t1));
        }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullの場合、Failureになります。</summary>
        public static Func<T1, Result<U, Unit>> NullToResultFunc<T1, U>(this Func<T1, U?> self)
            where U : struct
        {
            return (t1) => Result.Create(self(t1));
        }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullか例外の場合、Failureになります。nullの場合はNullResultExceptionがFailureの値として使用されます。</summary>
        public static Func<T1, Result<U, Exception>> ToResultFunc<T1, U>(this Func<T1, U> self)
        {
            return (t1) =>
            {
                try
                {
                    var res = self(t1);
                    if (res == null)
                        return Result.Failure((Exception)new NullResultException());
                    return Result.Success(res);
                } catch (Exception e) { return Result.Failure(e); }
            };
        }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullか例外の場合、Failureになります。nullの場合はNullResultExceptionがFailureの値として使用されます。</summary>
        public static Func<T1, Result<U, Exception>> ToResultFunc<T1, U>(this Func<T1, U?> self)
            where U : struct
        {
            return (t1) =>
            {
                try
                {
                    var res = self(t1);
                    if (res == null)
                        return Result.Failure((Exception)new NullResultException());
                    return Result.Success(res.Value);
                } catch (Exception e) { return Result.Failure(e); }
            };
        }

        /// <summary>関数の結果をResultで包む関数に変換します。</summary>
        public static Func<T1, T2, Result<U, Exception>> ExnToResultFunc<T1, T2, U>(this Func<T1, T2, U> self)
        {
            return (t1, t2) => { try { return Result.Success(self(t1, t2)); } catch (Exception e) { return Result.Failure(e); } };
        }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullの場合、Failureになります。</summary>
        public static Func<T1, T2, Result<U, Unit>> NullToResultFunc<T1, T2, U>(this Func<T1, T2, U> self)
            where U : class
        {
            return (t1, t2) => Result.Create(self(t1, t2));
        }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullの場合、Failureになります。</summary>
        public static Func<T1, T2, Result<U, Unit>> NullToResultFunc<T1, T2, U>(this Func<T1, T2, U?> self)
            where U : struct
        {
            return (t1, t2) => Result.Create(self(t1, t2));
        }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullか例外の場合、Failureになります。nullの場合はNullResultExceptionがFailureの値として使用されます。</summary>
        public static Func<T1, T2, Result<U, Exception>> ToResultFunc<T1, T2, U>(this Func<T1, T2, U> self)
        {
            return (t1, t2) =>
            {
                try
                {
                    var res = self(t1, t2);
                    if (res == null)
                        return Result.Failure((Exception)new NullResultException());
                    return Result.Success(res);
                } catch (Exception e) { return Result.Failure(e); }
            };
        }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullか例外の場合、Failureになります。nullの場合はNullResultExceptionがFailureの値として使用されます。</summary>
        public static Func<T1, T2, Result<U, Exception>> ToResultFunc<T1, T2, U>(this Func<T1, T2, U?> self)
            where U : struct
        {
            return (t1, t2) =>
            {
                try
                {
                    var res = self(t1, t2);
                    if (res == null)
                        return Result.Failure((Exception)new NullResultException());
                    return Result.Success(res.Value);
                } catch (Exception e) { return Result.Failure(e); }
            };
        }

        /// <summary>関数の結果をResultで包む関数に変換します。</summary>
        public static Func<T1, T2, T3, Result<U, Exception>> ExnToResultFunc<T1, T2, T3, U>(this Func<T1, T2, T3, U> self)
        {
            return (t1, t2, t3) => { try { return Result.Success(self(t1, t2, t3)); } catch (Exception e) { return Result.Failure(e); } };
        }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullの場合、Failureになります。</summary>
        public static Func<T1, T2, T3, Result<U, Unit>> NullToResultFunc<T1, T2, T3, U>(this Func<T1, T2, T3, U> self)
            where U : class
        {
            return (t1, t2, t3) => Result.Create(self(t1, t2, t3));
        }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullの場合、Failureになります。</summary>
        public static Func<T1, T2, T3, Result<U, Unit>> NullToResultFunc<T1, T2, T3, U>(this Func<T1, T2, T3, U?> self)
            where U : struct
        {
            return (t1, t2, t3) => Result.Create(self(t1, t2, t3));
        }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullか例外の場合、Failureになります。nullの場合はNullResultExceptionがFailureの値として使用されます。</summary>
        public static Func<T1, T2, T3, Result<U, Exception>> ToResultFunc<T1, T2, T3, U>(this Func<T1, T2, T3, U> self)
        {
            return (t1, t2, t3) =>
            {
                try
                {
                    var res = self(t1, t2, t3);
                    if (res == null)
                        return Result.Failure((Exception)new NullResultException());
                    return Result.Success(res);
                } catch (Exception e) { return Result.Failure(e); }
            };
        }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullか例外の場合、Failureになります。nullの場合はNullResultExceptionがFailureの値として使用されます。</summary>
        public static Func<T1, T2, T3, Result<U, Exception>> ToResultFunc<T1, T2, T3, U>(this Func<T1, T2, T3, U?> self)
            where U : struct
        {
            return (t1, t2, t3) =>
            {
                try
                {
                    var res = self(t1, t2, t3);
                    if (res == null)
                        return Result.Failure((Exception)new NullResultException());
                    return Result.Success(res.Value);
                } catch (Exception e) { return Result.Failure(e); }
            };
        }

        /// <summary>関数の結果をResultで包む関数に変換します。</summary>
        public static Func<T1, T2, T3, T4, Result<U, Exception>> ExnToResultFunc<T1, T2, T3, T4, U>(this Func<T1, T2, T3, T4, U> self)
        {
            return (t1, t2, t3, t4) => { try { return Result.Success(self(t1, t2, t3, t4)); } catch (Exception e) { return Result.Failure(e); } };
        }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullの場合、Failureになります。</summary>
        public static Func<T1, T2, T3, T4, Result<U, Unit>> NullToResultFunc<T1, T2, T3, T4, U>(this Func<T1, T2, T3, T4, U> self)
            where U : class
        {
            return (t1, t2, t3, t4) => Result.Create(self(t1, t2, t3, t4));
        }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullの場合、Failureになります。</summary>
        public static Func<T1, T2, T3, T4, Result<U, Unit>> NullToResultFunc<T1, T2, T3, T4, U>(this Func<T1, T2, T3, T4, U?> self)
            where U : struct
        {
            return (t1, t2, t3, t4) => Result.Create(self(t1, t2, t3, t4));
        }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullか例外の場合、Failureになります。nullの場合はNullResultExceptionがFailureの値として使用されます。</summary>
        public static Func<T1, T2, T3, T4, Result<U, Exception>> ToResultFunc<T1, T2, T3, T4, U>(this Func<T1, T2, T3, T4, U> self)
        {
            return (t1, t2, t3, t4) =>
            {
                try
                {
                    var res = self(t1, t2, t3, t4);
                    if (res == null)
                        return Result.Failure((Exception)new NullResultException());
                    return Result.Success(res);
                } catch (Exception e) { return Result.Failure(e); }
            };
        }

        /// <summary>関数の結果をResultで包む関数に変換します。関数の結果がnullか例外の場合、Failureになります。nullの場合はNullResultExceptionがFailureの値として使用されます。</summary>
        public static Func<T1, T2, T3, T4, Result<U, Exception>> ToResultFunc<T1, T2, T3, T4, U>(this Func<T1, T2, T3, T4, U?> self)
            where U : struct
        {
            return (t1, t2, t3, t4) =>
            {
                try
                {
                    var res = self(t1, t2, t3, t4);
                    if (res == null)
                        return Result.Failure((Exception)new NullResultException());
                    return Result.Success(res.Value);
                } catch (Exception e) { return Result.Failure(e); }
            };
        }

    }
}
