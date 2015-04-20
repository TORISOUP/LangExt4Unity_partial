﻿using System;
using System.ComponentModel;
using UniRx;
namespace LangExt
{
    partial class Create
    {
        /// <summary>
        /// valueがnullでない場合はOption.Some(value)と、
        /// valueがnullの場合はOption.Noneと同じオブジェクトを返します。
        /// </summary>
        /// <typeparam name="T">生成するOptionの要素の型</typeparam>
        /// <param name="value">値</param>
        /// <returns>valueがnullの場合None、そうでない場合Some</returns>
        public static Option<T> Option<T>(T value)
        {
            return LangExt.Option.Create(value);
        }

        /// <summary>
        /// valueがnullでない場合はOption.Some(value.Value)と、
        /// valueがnullの場合はOption.Noneと同じオブジェクトを返します。
        /// </summary>
        /// <typeparam name="T">生成するOptionの要素の型</typeparam>
        /// <param name="value">値</param>
        /// <returns>valueがnullの場合None、そうでない場合Some</returns>
        public static Option<T> Option<T>(T? value) where T : struct
        {
            return LangExt.Option.Create(value);
        }
    }

    /// <summary>
    /// Optionに対する関数を提供します。
    /// 例外を投げうる関数は、Unsafe名前空間のSeqモジュールで提供しています。
    /// </summary>
    public static partial class Option
    {
        /// <summary>
        /// valueがnullでない場合はOption.Some(value)と、
        /// valueがnullの場合はOption.Noneと同じオブジェクトを返します。
        /// </summary>
        /// <typeparam name="T">生成するOptionの要素の型</typeparam>
        /// <param name="value">値</param>
        /// <returns>valueがnullの場合None、そうでない場合Some</returns>
        public static Option<T> Create<T>(T value)
        {
            return value == null ? Option<T>.None : new Option<T>(value);
        }

        /// <summary>
        /// valueがnullでない場合はOption.Some(value.Value)と、
        /// valueがnullの場合はOption.Noneと同じオブジェクトを返します。
        /// </summary>
        /// <typeparam name="T">生成するOptionの要素の型</typeparam>
        /// <param name="value">値</param>
        /// <returns>valueがnullの場合None、そうでない場合Some</returns>
        public static Option<T> Create<T>(T? value) where T : struct
        {
            return value == null ? Option<T>.None : new Option<T>(value.Value);
        }

        /// <summary>
        /// valueを格納するOptionを生成します。
        /// valueとしてnullを格納することは出来ません。
        /// </summary>
        /// <typeparam name="T">Someに保持する値の型</typeparam>
        /// <param name="value">Someに保持する値</param>
        /// <returns>Some(value)</returns>
        public static Option<T> Some<T>(T value)
        {
            return new Option<T>(value);
        }

        /// <summary>
        /// Unitを格納するOptionを生成します。
        /// </summary>
        /// <returns>Some(Unit._)</returns>
        public static Option<Unit> Some()
        {
            return new Option<Unit>(Unit._);
        }

        /// <summary>
        /// 値がないことを表すOption型の値(None)を取得します。 
        /// </summary>
        public static Option<Placeholder> None
        {
            get
            {
                return Option<Placeholder>.None;
            }
        }

        /// <summary>
        /// 失敗しうる無引数関数からOptionを生成します。
        /// この関数は、例外を使用するAPIとOptionを使ったAPIの橋渡しをします。
        /// </summary>
        /// <typeparam name="T">関数が成功した場合の型(戻り値の型)</typeparam>
        /// <param name="f">実行する関数</param>
        /// <returns>関数が成功した場合、結果をSomeで包んだ値。失敗した場合、None。</returns>
        public static Option<T> FromFunc<T>(Func<T> f)
        {
            return f.ToOptionFunc()();
        }

        /// <summary>
        /// 値を持つかどうかを取得する関数が必要な時に使います。
        /// Option[T]がプロパティとしてIsSomeを持つため、拡張メソッドにはしていません。
        /// </summary>
        public static bool IsSome<T>(Option<T> self)
        {
            return self.IsSome;
        }

        /// <summary>
        /// 値を持たないかどうかを取得する関数が必要な時に使います。
        /// Option[T]がプロパティとしてIsNoneを持つため、拡張メソッドにはしていません。
        /// </summary>
        public static bool IsNone<T>(Option<T> self)
        {
            return self.IsNone;
        }
        
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

    /// <summary>
    /// nullよりも安全に「値がないこと」を表すことのできるデータ型です。
    /// </summary>
    /// <remarks>
    /// この型は値のある「Some」と、値がない「None」を表すことができ、
    /// デフォルトではNoneとなります。
    /// また、任意の型からの暗黙変換を定義しており、この場合を保持するSomeとなります。
    /// </remarks>
    /// <typeparam name="T">値の型</typeparam>
    public struct Option<T> : IEquatable<Option<T>>
    {
        readonly bool hasValue;
        readonly T value;

        /// <summary>
        /// 値を指定して明示的にSomeを生成します。
        /// 基本的にはこのコンストラクタを直接使用せずに、
        /// Option.Someメソッドを使用してください。
        /// 引数にnullを指定すると、ArgumentNullExceptionが投げられます。
        /// これにより、Optionが値を持つ場合、その値がnullではないことが保証されます。
        /// </summary>
        /// <param name="value">Optionに格納する値</param>
        public Option(T value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            this.hasValue = true;
            this.value = value;
        }

        /// <summary>
        /// 任意の型のNoneを取得します。
        /// 基本的にはこのプロパティを直接使用せずに、
        /// Option.Noneプロパティを使用してください。
        /// </summary>
        public static Option<T> None
        {
            get { return default(Option<T>); }
        }

        /// <summary>
        /// Option.Noneで取得したオブジェクトを、任意のTのNoneに暗黙変換します。
        /// </summary>
        /// <param name="value">Option.None</param>
        /// <returns>任意のTのNone</returns>
        public static implicit operator Option<T>(Option<Placeholder> value)
        {
            return Option<T>.None;
        }

        internal T Value { get { return this.value; } }

        /// <summary>
        /// Someの場合、保持された値を取り出します。
        /// Noneの場合、引数に指定した値を返します。
        /// </summary>
        /// <param name="defaultValue">Noneの場合の戻り値</param>
        /// <returns>Someの場合保持している値。Noneの場合引数で指定した値</returns>
        public T GetOr(T defaultValue)
        {
            return this.hasValue ? this.value : defaultValue;
        }

        /// <summary>
        /// Someの場合、保持された値を取り出します。
        /// Noneの場合、引数に指定した関数の実行結果を返します。
        /// </summary>
        /// <param name="defaultF">Noneの場合の戻り値を返す関数</param>
        /// <returns>Someの場合保持している値。Noneの場合引数で指定した関数が返す値</returns>
        public T GetOrElse(Func<T> defaultF)
        {
            return this.hasValue ? this.value : defaultF();
        }

        /// <summary>
        /// 擬似的にパターンマッチを行います。
        /// 値がある場合とない場合の両方で何らかの処理を行う必要がある際に使用します。
        /// </summary>
        /// <typeparam name="U">パターンマッチが返す処理の型</typeparam>
        /// <param name="Some">Someの場合の処理</param>
        /// <param name="None">Noneの場合の処理</param>
        /// <returns>処理が返した値</returns>
        public U Match<U>(Func<T, U> Some, Func<U> None)
        {
            return this.hasValue ? Some(this.value) : None();
        }

        /// <summary>
        /// 擬似的にパターンマッチを行います。
        /// 値がある場合とない場合の両方で何らかの処理を行う必要がある際に使用します。
        /// </summary>
        /// <param name="Some">Someの場合の処理</param>
        /// <param name="None">Noneの場合の処理</param>
        public void Match(Action<T> Some, Action None)
        {
            if (this.hasValue)
                Some(this.value);
            else
                None();
        }

        /// <summary>
        /// T型のOptionをU型のオプションに変換します。
        /// 値がある場合のみに変換を適用します。
        /// 変換は、T型を受け取りU型のOptionを返す関数として指定します。
        /// </summary>
        /// <typeparam name="U">変換先の型</typeparam>
        /// <param name="f">変換に用いる関数</param>
        /// <returns>変換した値</returns>
        public Option<U> Bind<U>(Func<T, Option<U>> f)
        {
            return this.hasValue ? f(this.value) : Option<U>.None;
        }

        /// <summary>
        /// 現在のオブジェクトが、同じ型の別のオブジェクトと等しいかどうかを判定します。
        /// Option.Noneで返された値と、型付きのNoneを比較した場合にfalseが返される点に注意してください。
        /// もしその場合にtrueを返してほしい場合は、EqualsではなくNonStrictEqualsを使用してください。
        /// </summary>
        /// <param name="other">このオブジェクトと比較するOption</param>
        /// <returns>現在のオブジェクトがotherで指定されたオブジェクトと等しい場合はtrue、それ以外の場合はfalse</returns>
        public bool Equals(Option<T> other)
        {
            return this.hasValue == other.hasValue && Equals(this.value, other.value);
        }

        /// <summary>
        /// 現在のオブジェクトが、別のOptionと等しいかどうかを判定します。
        /// Equalsとは違い、Option.Noneで返された値と型付きのNoneを比較した場合にもtrueを返します。
        /// </summary>
        /// <param name="other">このオブジェクトと比較するOption</param>
        /// <returns>現在のオブジェクトがotherで指定されたオブジェクトと等しい場合はtrue、それ以外の場合はfalse</returns>
        public bool NonStrictEquals<U>(Option<U> other)
        {
            if (this.hasValue == false)
                return other.hasValue == false;
            return Equals(this.value, other.value);
        }

        /// <summary>
        /// 現在のオブジェクトが、別のオブジェクトと等しいかどうかを判定します。
        /// </summary>
        /// <param name="obj">このオブジェクトと比較するオブジェクト</param>
        /// <returns>現在のオブジェクトがobjで指定されたオブジェクトと等しい場合はtrue、それ以外の場合はfalse</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj)
        {
            if (obj.IsNull())
                return false;

            var type = obj.GetType();
            if (type.IsGenericType == false)
                return false;

            var genType = type.GetGenericTypeDefinition();
            if (genType != typeof(Option<>))
                return false;

            if ((obj is Option<T>) == false)
                return false;
            return Equals((Option<T>)obj);
        }

        /// <summary>
        /// 2つのOptionの比較を行います。 
        /// </summary>
        /// <param name="a">1つ目のOption</param>
        /// <param name="b">2つ目のOption</param>
        /// <returns>2つのOptionが等しい場合はtrue、それ以外の場合はfalse</returns>
        public static bool operator ==(Option<T> a, Option<T> b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// 2つのOptionの比較を行います。 
        /// </summary>
        /// <param name="a">1つ目のOption</param>
        /// <param name="b">2つ目のOption</param>
        /// <returns>2つのOptionが等しい場合はfalse、それ以外の場合はtrue</returns>
        public static bool operator !=(Option<T> a, Option<T> b)
        {
            return !(a == b);
        }

        /// <summary>
        /// このオブジェクトが値を持つかどうかを取得します。
        /// 制御構文内でboolが必要な場所に対しては、IsSomeやIsNoneではなく、Optionオブジェクトをそのまま使用することも可能です。
        /// </summary>
        public bool IsSome
        {
            get { return this.hasValue; }
        }

        /// <summary>
        /// このオブジェクトが値を持たないかどうかを取得します。
        /// 制御構文内でboolが必要な場所に対しては、IsSomeやIsNoneではなく、Optionオブジェクトをそのまま使用することも可能です。
        /// </summary>
        public bool IsNone
        {
            get { return this.hasValue == false; }
        }

        /// <summary>
        /// Optionが値を持つかどうかを判定します。
        /// </summary>
        /// <param name="x">判定の対象</param>
        /// <returns>値を持つ場合はtrue、持たない場合はfalse</returns>
        public static bool operator true(Option<T> x)
        {
            return x.hasValue;
        }

        /// <summary>
        /// Optionが値を持たないかどうかを判定します。
        /// </summary>
        /// <param name="x">判定の対象</param>
        /// <returns>値を持つ場合はfalse、持たない場合はtrue</returns>
        public static bool operator false(Option<T> x)
        {
            return x.hasValue == false;
        }

        /// <summary>
        /// 自身が値を持つ場合は自身を、そうでない場合はelsePartの結果を返します。
        /// Optionは短絡のor演算子を提供しているため、そちらを使用したほうが効率的です。
        /// </summary>
        /// <param name="elsePart">自身が値を持たなかった場合の値を返す関数</param>
        /// <returns>自身が値を持つ場合は自身、値を持たない場合はelsePartの結果</returns>
        public Option<T> OrElse(Func<Option<T>> elsePart)
        {
            return this.hasValue ? this : elsePart();
        }

        /// <summary>
        /// 自身とthenPartの結果の両方が値を持つ場合はthenPartの結果を、
        /// そうでない場合はNoneを返します。
        /// Optionは短絡のand演算子を提供しているため、そちらを使用したほうが効率的です。
        /// </summary>
        /// <param name="thenPart">自身が値を持つ場合の値を返す関数</param>
        /// <returns>自身とthenPartの結果の両方が値を持つ場合はthenPartの結果、どちらか一方でも値を持たなかった場合はNone</returns>
        public Option<U> AndThen<U>(Func<Option<U>> thenPart)
        {
            return this.hasValue == false ? Option<U>.None : thenPart();
        }

        /// <summary>
        /// 左辺のOptionが値を持つ場合は左辺を、そうでない場合は右辺を返します。
        /// 通常、直接使用せず、短絡演算子として使用します。
        /// </summary>
        /// <param name="a">1つ目のOption</param>
        /// <param name="b">2つ目のOption</param>
        /// <returns>左辺のOptionが値を持つ場合は左辺、それ以外の場合は右辺</returns>
        public static Option<T> operator |(Option<T> a, Option<T> b)
        {
            return a.hasValue ? a : b;
        }

        /// <summary>
        /// 両辺のOptionが値を持つ場合は右辺を、そうでない場合はNoneを返します。
        /// 通常、直接使用せず、短絡演算子として使用します。
        /// </summary>
        /// <param name="a">1つ目のOption</param>
        /// <param name="b">2つ目のOption</param>
        /// <returns>両辺のOptionが値を持つ場合は右辺を、一方でも値を持たない場合はNone</returns>
        public static Option<T> operator &(Option<T> a, Option<T> b)
        {
            return a.hasValue == false ? Option<T>.None : b;
        }

        /// <summary>
        /// オブジェクトのハッシュコードを取得します。
        /// </summary>
        /// <returns>ハッシュコード</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            var result = 31;
            result ^= this.hasValue.GetHashCode();
            result ^= this.value.IsNull() ? 0 : this.value.GetHashCode();
            return result;
        }

        /// <summary>
        /// このオブジェクトを文字列表現に変換します。
        /// </summary>
        /// <returns>このオブジェクトの文字列表現</returns>
        public override string ToString()
        {
            return this.hasValue ? string.Concat("Some(", this.value, ")") : "None";
        }
        
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
}
