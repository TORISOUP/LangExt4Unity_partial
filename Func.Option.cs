
using System;

namespace LangExt
{
    static partial class Func
    {
        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果が例外の場合、Noneになります。</summary>
        public static Func<Option<T>> ExnToOptionFunc<T>(this Func<T> self)
        {
            return () => { try { return Option.Some(self()); } catch { return Option.None; } };
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullの場合、Noneになります。</summary>
        public static Func<Option<T>> NullToOptionFunc<T>(this Func<T> self)
            where T : class
        {
            return () => Option.Create(self());
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullの場合、Noneになります。</summary>
        public static Func<Option<T>> NullToOptionFunc<T>(this Func<T?> self)
            where T : struct
        {
            return () => Option.Create(self());
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullか例外の場合、Noneになります。</summary>
        public static Func<Option<T>> ToOptionFunc<T>(this Func<T> self)
        {
            return () => { try { return Option.Create(self()); } catch { return Option.None; } };
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullか例外の場合、Noneになります。</summary>
        public static Func<Option<T>> ToOptionFunc<T>(this Func<T?> self)
            where T : struct
        {
            return () => { try { return Option.Create(self()); } catch { return Option.None; } };
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果が例外の場合、Noneになります。</summary>
        public static Func<T1, Option<U>> ExnToOptionFunc<T1, U>(this Func<T1, U> self)
        {
            return (t1) => { try { return Option.Some(self(t1)); } catch { return Option.None; } };
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullの場合、Noneになります。</summary>
        public static Func<T1, Option<U>> NullToOptionFunc<T1, U>(this Func<T1, U> self)
            where U : class
        {
            return (t1) => Option.Create(self(t1));
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullの場合、Noneになります。</summary>
        public static Func<T1, Option<U>> NullToOptionFunc<T1, U>(this Func<T1, U?> self)
            where U : struct
        {
            return (t1) => Option.Create(self(t1));
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullか例外の場合、Noneになります。</summary>
        public static Func<T1, Option<U>> ToOptionFunc<T1, U>(this Func<T1, U> self)
        {
            return (t1) => { try { return Option.Create(self(t1)); } catch { return Option.None; } };
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullか例外の場合、Noneになります。</summary>
        public static Func<T1, Option<U>> ToOptionFunc<T1, U>(this Func<T1, U?> self)
            where U : struct
        {
            return (t1) => { try { return Option.Create(self(t1)); } catch { return Option.None; } };
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果が例外の場合、Noneになります。</summary>
        public static Func<T1, T2, Option<U>> ExnToOptionFunc<T1, T2, U>(this Func<T1, T2, U> self)
        {
            return (t1, t2) => { try { return Option.Some(self(t1, t2)); } catch { return Option.None; } };
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullの場合、Noneになります。</summary>
        public static Func<T1, T2, Option<U>> NullToOptionFunc<T1, T2, U>(this Func<T1, T2, U> self)
            where U : class
        {
            return (t1, t2) => Option.Create(self(t1, t2));
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullの場合、Noneになります。</summary>
        public static Func<T1, T2, Option<U>> NullToOptionFunc<T1, T2, U>(this Func<T1, T2, U?> self)
            where U : struct
        {
            return (t1, t2) => Option.Create(self(t1, t2));
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullか例外の場合、Noneになります。</summary>
        public static Func<T1, T2, Option<U>> ToOptionFunc<T1, T2, U>(this Func<T1, T2, U> self)
        {
            return (t1, t2) => { try { return Option.Create(self(t1, t2)); } catch { return Option.None; } };
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullか例外の場合、Noneになります。</summary>
        public static Func<T1, T2, Option<U>> ToOptionFunc<T1, T2, U>(this Func<T1, T2, U?> self)
            where U : struct
        {
            return (t1, t2) => { try { return Option.Create(self(t1, t2)); } catch { return Option.None; } };
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果が例外の場合、Noneになります。</summary>
        public static Func<T1, T2, T3, Option<U>> ExnToOptionFunc<T1, T2, T3, U>(this Func<T1, T2, T3, U> self)
        {
            return (t1, t2, t3) => { try { return Option.Some(self(t1, t2, t3)); } catch { return Option.None; } };
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullの場合、Noneになります。</summary>
        public static Func<T1, T2, T3, Option<U>> NullToOptionFunc<T1, T2, T3, U>(this Func<T1, T2, T3, U> self)
            where U : class
        {
            return (t1, t2, t3) => Option.Create(self(t1, t2, t3));
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullの場合、Noneになります。</summary>
        public static Func<T1, T2, T3, Option<U>> NullToOptionFunc<T1, T2, T3, U>(this Func<T1, T2, T3, U?> self)
            where U : struct
        {
            return (t1, t2, t3) => Option.Create(self(t1, t2, t3));
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullか例外の場合、Noneになります。</summary>
        public static Func<T1, T2, T3, Option<U>> ToOptionFunc<T1, T2, T3, U>(this Func<T1, T2, T3, U> self)
        {
            return (t1, t2, t3) => { try { return Option.Create(self(t1, t2, t3)); } catch { return Option.None; } };
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullか例外の場合、Noneになります。</summary>
        public static Func<T1, T2, T3, Option<U>> ToOptionFunc<T1, T2, T3, U>(this Func<T1, T2, T3, U?> self)
            where U : struct
        {
            return (t1, t2, t3) => { try { return Option.Create(self(t1, t2, t3)); } catch { return Option.None; } };
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果が例外の場合、Noneになります。</summary>
        public static Func<T1, T2, T3, T4, Option<U>> ExnToOptionFunc<T1, T2, T3, T4, U>(this Func<T1, T2, T3, T4, U> self)
        {
            return (t1, t2, t3, t4) => { try { return Option.Some(self(t1, t2, t3, t4)); } catch { return Option.None; } };
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullの場合、Noneになります。</summary>
        public static Func<T1, T2, T3, T4, Option<U>> NullToOptionFunc<T1, T2, T3, T4, U>(this Func<T1, T2, T3, T4, U> self)
            where U : class
        {
            return (t1, t2, t3, t4) => Option.Create(self(t1, t2, t3, t4));
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullの場合、Noneになります。</summary>
        public static Func<T1, T2, T3, T4, Option<U>> NullToOptionFunc<T1, T2, T3, T4, U>(this Func<T1, T2, T3, T4, U?> self)
            where U : struct
        {
            return (t1, t2, t3, t4) => Option.Create(self(t1, t2, t3, t4));
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullか例外の場合、Noneになります。</summary>
        public static Func<T1, T2, T3, T4, Option<U>> ToOptionFunc<T1, T2, T3, T4, U>(this Func<T1, T2, T3, T4, U> self)
        {
            return (t1, t2, t3, t4) => { try { return Option.Create(self(t1, t2, t3, t4)); } catch { return Option.None; } };
        }

        /// <summary>関数の結果をOptionで包む関数に変換します。関数の結果がnullか例外の場合、Noneになります。</summary>
        public static Func<T1, T2, T3, T4, Option<U>> ToOptionFunc<T1, T2, T3, T4, U>(this Func<T1, T2, T3, T4, U?> self)
            where U : struct
        {
            return (t1, t2, t3, t4) => { try { return Option.Create(self(t1, t2, t3, t4)); } catch { return Option.None; } };
        }

    }
}
