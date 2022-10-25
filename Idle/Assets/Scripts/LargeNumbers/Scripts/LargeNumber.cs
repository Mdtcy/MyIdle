// ----------------------------------------------------------------------------
// LargeNumbers.LargeNumber
// (c) Milan Egon Votrubec
//
// For documentation, please see the included "Large Numbers Library Welcome.pdf" file.
//
// For example usage, please see the "largenumbers.unity" example scene.
//
// Please see the "license.txt" file for licensing.
// ----------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace LargeNumbers
{
    /// <summary>
    /// A Conway & Guy Latin based number.
    /// </summary>
#if UNITY_EDITOR || UNITY_STANDALONE
    [Serializable]
#endif
    public struct LargeNumber : IEquatable<LargeNumber>
    {
        internal readonly struct Prefix
        {
            internal readonly string Name;
            internal readonly UnitPrefixModifiers Modifier;

            internal Prefix ( string Name, UnitPrefixModifiers Modifier )
            {
                this.Name = Name;
                this.Modifier = Modifier;
            }
        }


        // ---------------------------------------------------------------------------- Static Variables and Methods

        private static StringBuilder sb = new StringBuilder ( 44 );

        // This list helps reduce Maths.Pow calls, but also idicates the precision of mathematic operations. Values that are deemed too far different in scale are ignored.
        // I honestly can't believe any idle/clicker game is going to ever care about values this far apart.
        private static readonly double [ ] Powers = {
            1,
            1_000d,
            1_000_000d,
            1_000_000_000d,
            1_000_000_000_000d,
            1_000_000_000_000_000d,
            1_000_000_000_000_000_000d,
            1_000_000_000_000_000_000_000d,
            1_000_000_000_000_000_000_000_000d,
            1_000_000_000_000_000_000_000_000_000d,
            1_000_000_000_000_000_000_000_000_000_000d,
            1_000_000_000_000_000_000_000_000_000_000_000d,
            1_000_000_000_000_000_000_000_000_000_000_000_000d,
            1_000_000_000_000_000_000_000_000_000_000_000_000_000d,
            1_000_000_000_000_000_000_000_000_000_000_000_000_000_000d,
            1_000_000_000_000_000_000_000_000_000_000_000_000_000_000_000d
        };

        [Flags]
        internal enum UnitPrefixModifiers
        {
            None = 0,
            S = 1 << 0,
            X = 1 << 1,
            M = 1 << 2,
            N = 1 << 3,
            SX = S | X,
            SM = S | M,
            SN = S | N,
            XM = X | M,
            XN = X | N,
            MN = M | N
        }

        static internal readonly string [ ] StandardNames =
        {
            "",
            "Thousand",
            "Million",
            "Billion",
            "Trillion",
            "Quadrillion",
            "Quintillion",
            "Sextillion",
            "Septillion",
            "Octillion",
            "Nonillion",
            "Decillion",
        };

        static internal readonly Prefix [ ] [ ] Prefixes =
        {
            // Units
            new Prefix [] {
                new Prefix ( Name : string.Empty, Modifier : UnitPrefixModifiers.None  ),
                new Prefix ( Name : "Un", Modifier : UnitPrefixModifiers.None  ),
                new Prefix ( Name : "Duo", Modifier : UnitPrefixModifiers.None  ),
                new Prefix ( Name : "Tre", Modifier : UnitPrefixModifiers.S  ),
                new Prefix ( Name : "Quattuor", Modifier : UnitPrefixModifiers.None  ),
                new Prefix ( Name : "Quinqua", Modifier : UnitPrefixModifiers.None  ),
                new Prefix ( Name : "Se", Modifier : UnitPrefixModifiers.SX  ),
                new Prefix ( Name : "Septe", Modifier : UnitPrefixModifiers.MN  ),
                new Prefix ( Name : "Octo", Modifier : UnitPrefixModifiers.None  ),
                new Prefix ( Name : "Nove", Modifier : UnitPrefixModifiers.MN  )
            },
            // Tens
            new Prefix [] {
                new Prefix ( Name : string.Empty, Modifier : UnitPrefixModifiers.None  ),
                new Prefix ( Name : "Deci", Modifier : UnitPrefixModifiers.N  ),
                new Prefix ( Name : "Viginti", Modifier : UnitPrefixModifiers.SM  ),
                new Prefix ( Name : "Triginta", Modifier : UnitPrefixModifiers.SN  ),
                new Prefix ( Name : "Quadraginta", Modifier : UnitPrefixModifiers.SN  ),
                new Prefix ( Name : "Quinquaginta", Modifier : UnitPrefixModifiers.SN  ),
                new Prefix ( Name : "Sexaginta", Modifier : UnitPrefixModifiers.N  ),
                new Prefix ( Name : "Septuaginta", Modifier : UnitPrefixModifiers.N  ),
                new Prefix ( Name : "Octoginta", Modifier : UnitPrefixModifiers.XM  ),
                new Prefix ( Name : "Nonaginta", Modifier : UnitPrefixModifiers.None  )
            },
            // Hundreds
            new Prefix [] {
                new Prefix ( Name : string.Empty, Modifier : UnitPrefixModifiers.None  ),
                new Prefix ( Name : "Centi", Modifier : UnitPrefixModifiers.XN  ),
                new Prefix ( Name : "Ducenti", Modifier : UnitPrefixModifiers.N  ),
                new Prefix ( Name : "Trecenti", Modifier : UnitPrefixModifiers.SN  ),
                new Prefix ( Name : "Quadringenti", Modifier : UnitPrefixModifiers.SN  ),
                new Prefix ( Name : "Quingenti", Modifier : UnitPrefixModifiers.SN  ),
                new Prefix ( Name : "Sescenti", Modifier : UnitPrefixModifiers.N  ),
                new Prefix ( Name : "Septingenti", Modifier : UnitPrefixModifiers.N  ),
                new Prefix ( Name : "Octingenti", Modifier : UnitPrefixModifiers.XM  ),
                new Prefix ( Name : "Nongenti", Modifier : UnitPrefixModifiers.None  )
            }
        };


        /// <summary>
        /// This method allows the developer to get just the large number name (base name). It's especially helpful 
        /// when the value and name are going to be displayed differently in the UI.
        /// </summary>
        /// <param name="base">The short scale base value. Synonymous with magnitude,</param>
        private static StringBuilder largeNumberName = new StringBuilder ( 40 );
        public static string GetLargeNumberName ( int @base )
        {
            if ( @base >= 1001 ) return "Humongous!";
            if ( @base <= -1001 ) return "Humongouth!";
            var absBase = @base < 0 ? -@base : @base;
            if ( absBase < 2 )
                return string.Empty;

            largeNumberName.Clear ( );

            if ( absBase < StandardNames.Length )
            {
                largeNumberName.Append ( StandardNames [ absBase ] );
                if ( @base < 0 ) largeNumberName.Append ( "th" );
                return largeNumberName.ToString ( );
            }

            --absBase;
            var hundreds = absBase / 100;
            var newBase = absBase - ( hundreds * 100 );
            var tens = newBase / 10;
            newBase -= ( tens * 10 ); // is now the units

            var unitsModifier = UnitPrefixModifiers.None;
            if ( newBase != 0 )
            {
                unitsModifier = Prefixes [ 0 ] [ newBase ].Modifier;
                largeNumberName.Append ( Prefixes [ 0 ] [ newBase ].Name );
            }

            if ( tens > 0 )
            {
                if ( unitsModifier != UnitPrefixModifiers.None )
                {
                    var modiferIntersection = unitsModifier & Prefixes [ 1 ] [ tens ].Modifier;
                    switch ( modiferIntersection )
                    {
                        case UnitPrefixModifiers.S:
                            largeNumberName.Append ( "s" );
                            break;
                        case UnitPrefixModifiers.X:
                            largeNumberName.Append ( "x" );
                            break;
                        case UnitPrefixModifiers.M:
                            largeNumberName.Append ( "m" );
                            break;
                        case UnitPrefixModifiers.N:
                            largeNumberName.Append ( "n" );
                            break;
                    }
                    unitsModifier = UnitPrefixModifiers.None;
                }
                largeNumberName.Append ( Prefixes [ 1 ] [ tens ].Name );
            }

            if ( hundreds > 0 )
            {
                if ( unitsModifier != UnitPrefixModifiers.None )
                {
                    var modiferIntersection = unitsModifier & Prefixes [ 2 ] [ hundreds ].Modifier;
                    switch ( modiferIntersection )
                    {
                        case UnitPrefixModifiers.S:
                            largeNumberName.Append ( "s" );
                            break;
                        case UnitPrefixModifiers.X:
                            largeNumberName.Append ( "x" );
                            break;
                        case UnitPrefixModifiers.M:
                            largeNumberName.Append ( "m" );
                            break;
                        case UnitPrefixModifiers.N:
                            largeNumberName.Append ( "n" );
                            break;
                    }
                }
                largeNumberName.Append ( Prefixes [ 2 ] [ hundreds ].Name );
            }
            largeNumberName.Remove ( largeNumberName.Length - 1, 1 );
            largeNumberName.Append ( "illion" );

            if ( @base < 0 )
                largeNumberName.Append ( "th" );
            return largeNumberName.ToString ( );
        }


        // ---------------------------------------------------------------------------- Variables
        /// <summary>
        /// The coefficient is the value, or the number, that preceeds the latin name.
        /// </summary>
        public double coefficient;

        /// <summary>
        /// The magnitude refers to the short scale base (+1) of the large number. In scientic notation it is : coefficient x 10^(3 x magnitude).
        /// </summary>
        public int magnitude;


        // ---------------------------------------------------------------------------- Methods
        /// <summary>
        /// Create a new LargeNumber using a double value as the starting value.
        /// </summary>
        /// <param name="value"></param>
        public LargeNumber ( double value )
        {
            if ( value == 0 || double.IsInfinity ( value ) )
            {
                coefficient = 0;
                magnitude = 0;
                return;
            }

            var m = 0;
            Fix ( ref value, ref m );

            coefficient = value;
            magnitude = m;
        }

        /// <summary>
        /// Create a new LargeNumber.
        /// </summary>
        /// <param name="coefficient">The coefficient for the new LargeNumber.</param>
        /// <param name="magnitude">The magnitude for the new LargeNumber.</param>
        public LargeNumber ( double coefficient, int magnitude )
        {
            if ( coefficient == 0 || double.IsInfinity ( coefficient ) )
            {
                this.coefficient = 0;
                this.magnitude = 0;
                return;
            }

            Fix ( ref coefficient, ref magnitude );
            this.coefficient = coefficient;
            this.magnitude = magnitude;
        }


        /// <summary>
        /// Returns the LargeNumber value as a "standard" double data type.
        /// </summary>
        /// <returns>The double type representation. Be aware the the result might be +- Infinity if the value overflows.</returns>
        /// <remarks>Not a particularly useful method.</remarks>
        public double Standard ( )
        {
            if ( magnitude == 0 )
                return coefficient;

            if ( coefficient == 0 )
                return 0d;

            return Math.Pow ( 10, 3 * magnitude ) * coefficient;
        }


        /// <summary>
        /// Because the string representation of this number could be displayed in so many different ways, this method is used
        /// mainly to get the most basic representation of a Conway & Guy latin based number. Think debugging purposes.
        /// Type-Conversion (changing a numeric value to a string) will pretty much guarantee the production of garbage.
        /// </summary>
        public override string ToString ( )
        {
            sb.Clear ( );
            var absMagnitude = magnitude < 0 ? -magnitude : magnitude;
            if ( absMagnitude < 2 )
            {
                if ( magnitude > 0 )
                {
                    sb.ConvertAndAppend ( coefficient * Powers [ magnitude ], 3, false );
                    return sb.ToString ( );
                }

                sb.ConvertAndAppend ( coefficient / Powers [ -magnitude ], 3, false );
                return sb.ToString ( );
            }

            sb.ConvertAndAppend ( coefficient, 3, false ).Append ( ' ' );
            sb.Append ( GetLargeNumberName ( magnitude ) );
            return sb.ToString ( );
        }

        public static LargeNumber zero => new LargeNumber ( );
        public bool isZero => coefficient == 0;


        // ---------------------------------------------------------------------------- Compare, Equatable

        /// <summary>
        /// Checks to see how this numbers compares to another numbers.
        /// </summary>
        /// <param name="other">The other numbers.</param>
        /// <returns>0 if the two numbers are the same*. -1 if this numbers smaller. 1 if this numbers is larger.</returns>
        public int CompareTo ( LargeNumber other )
        {
            // Difference in coefficient signs. Instant giveaway.
            if ( other.coefficient <= 0 && coefficient > 0 ) return 1;
            if ( other.coefficient >= 0 && coefficient < 0 ) return -1;

            // Same sign values, so now check the magnitudes.
            var magDiff = magnitude - other.magnitude;
            var thisCoefficient = coefficient;
            var otherCoefficient = other.coefficient;
            if ( magDiff > 0 )
            {
                if ( magDiff >= Powers.Length ) thisCoefficient = double.MaxValue;
                else thisCoefficient *= Powers [ magDiff ];
            }
            else if ( magDiff < 0 )
            {
                if ( -magDiff >= Powers.Length ) otherCoefficient = double.MaxValue;
                else otherCoefficient *= Powers [ -magDiff ];
            }

            if ( thisCoefficient > otherCoefficient ) return 1;
            if ( thisCoefficient < otherCoefficient ) return -1;
            return 0;


            /*
            // NOTE: When fractional coefficients are valid, the following code, while more efficient, is flawed.
            if ( magnitude > other.magnitude ) return 1;
            if ( magnitude < other.magnitude ) return -1;

            // Same magnitudes, so now we check the actual coefficient values.
            var c1 = ( int ) ( coefficient * 1000000 );
            var c2 = ( int ) ( other.coefficient * 1000000 );
            if ( c1 > c2 ) return 1;
            if ( c1 < c2 ) return -1;
            return 0;
            */
        }


        /// <summary>
        /// Determine if this number is the same as another.
        /// </summary>
        /// <param name="other">The other number.</param>
        /// <returns>True if the two number are the same to within three decimal places.</returns>
        public bool Equals ( LargeNumber other )
        {
            var magnitudeDifference = other.magnitude - magnitude;
            // Now performs an equality check with numbers up to 1 order of magnitude difference.
            if ( magnitudeDifference < -1 || magnitudeDifference > 1 ) return false;
            //if ( other.magnitude != magnitude ) return false;
            var c1 = ( int ) ( coefficient * 1_000 * ( magnitudeDifference == -1 ? 1_000 : 1 ) );
            var c2 = ( int ) ( other.coefficient * 1_000 * ( magnitudeDifference == 1 ? 1_000 : 1 ) );
            return c1 == c2;
        }


        public override bool Equals ( object obj )
        {
            return obj is LargeNumber other && Equals ( other );
        }


        public override int GetHashCode ( )
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + coefficient.GetHashCode ( );
                hash = hash * 23 + magnitude.GetHashCode ( );
                return hash;
            }
        }


        // ---------------------------------------------------------------------------- Operator Overloading

        public static LargeNumber operator + ( LargeNumber a, LargeNumber b )
        {
            var magnitudeDifference = a.magnitude - b.magnitude;
            var absmagnitudeDifference = magnitudeDifference < 0 ? -magnitudeDifference : magnitudeDifference;

            var returnCoefficient = a.coefficient;
            var returnMagnitude = a.magnitude;

            if ( magnitudeDifference == 0 )
            {
                // The magnitudes are the same, so we can just add the values together.
                returnCoefficient += b.coefficient;
            }
            else
            {
                if ( magnitudeDifference < 0 )
                {
                    // a is the smaller of the two numbers.
                    if ( absmagnitudeDifference < Powers.Length )
                        returnCoefficient = ( returnCoefficient / Powers [ absmagnitudeDifference ] ) + b.coefficient;
                    else
                        returnCoefficient = b.coefficient;
                    returnMagnitude = b.magnitude;
                }
                else
                {
                    // b is the smaller of the two numbers.
                    if ( absmagnitudeDifference < Powers.Length )
                        returnCoefficient += ( b.coefficient / Powers [ absmagnitudeDifference ] );
                }
            }
            return new LargeNumber ( returnCoefficient, returnMagnitude );
        }


        public static LargeNumber operator - ( LargeNumber a, LargeNumber b )
        {
            var magnitudeDifference = a.magnitude - b.magnitude;
            var absmagnitudeDifference = magnitudeDifference < 0 ? -magnitudeDifference : magnitudeDifference;

            var returnCoefficient = a.coefficient;
            var returnMagnitude = a.magnitude;

            if ( magnitudeDifference == 0 )
            {
                // The magnitudes are the same, so we can just subtract the values together.
                returnCoefficient -= b.coefficient;
            }
            else
            {
                if ( magnitudeDifference < 0 )
                {
                    // a is the smaller of the two numbers.
                    if ( absmagnitudeDifference < Powers.Length )
                        returnCoefficient = ( a.coefficient / Powers [ absmagnitudeDifference ] ) - b.coefficient;
                    else
                        returnCoefficient = b.coefficient;
                    returnMagnitude = b.magnitude;
                }
                else
                {
                    // b is the smaller of the two numbers.
                    if ( absmagnitudeDifference < Powers.Length )
                        returnCoefficient -= ( b.coefficient / Powers [ absmagnitudeDifference ] );
                }
            }
            return new LargeNumber ( returnCoefficient, returnMagnitude );
        }


        public static LargeNumber operator + ( LargeNumber a, double b ) => a + new LargeNumber ( b );
        public static LargeNumber operator + ( double a, LargeNumber b ) => new LargeNumber ( a ) + b;
        public static LargeNumber operator - ( LargeNumber a, double b ) => a - new LargeNumber ( b );
        public static LargeNumber operator - ( double a, LargeNumber b ) => new LargeNumber ( a ) - b;

        public static LargeNumber operator * ( LargeNumber a, LargeNumber b )=> new LargeNumber ( a.coefficient * b.coefficient, a.magnitude + b.magnitude );
        public static LargeNumber operator * ( LargeNumber a, double b ) => new LargeNumber ( a.coefficient * b, a.magnitude );
        public static LargeNumber operator * ( double a, LargeNumber b ) => new LargeNumber ( a * b.coefficient, b.magnitude );
        public static LargeNumber operator / ( LargeNumber a, LargeNumber b )=> new LargeNumber ( a.coefficient / b.coefficient, a.magnitude - b.magnitude );
        public static LargeNumber operator / ( LargeNumber a, double b ) => new LargeNumber ( a.coefficient / b, a.magnitude );
        public static LargeNumber operator / ( double a, LargeNumber b ) => new LargeNumber ( a / b.coefficient, b.magnitude );

        public static bool operator < ( LargeNumber a, LargeNumber b ) { return a.CompareTo ( b ) == -1; }
        public static bool operator <= ( LargeNumber a, LargeNumber b ) { return a.CompareTo ( b ) <= 0; }
        public static bool operator > ( LargeNumber a, LargeNumber b ) { return a.CompareTo ( b ) == 1; }
        public static bool operator >= ( LargeNumber a, LargeNumber b ) { return a.CompareTo ( b ) >= 0; }


        public static bool operator == ( LargeNumber a, LargeNumber b )
        {
            return a.Equals ( b );
        }


        public static bool operator != ( LargeNumber a, LargeNumber b )
        {
            return !a.Equals ( b );
        }

        public string ToValuesString ( ) => $"(c:{coefficient} m:{magnitude})";

        public static implicit operator ScientificNotation ( LargeNumber number ) => new ScientificNotation ( number.coefficient, number.magnitude * 3 );
        public static implicit operator AlphabeticNotation ( LargeNumber number ) => new AlphabeticNotation ( number.coefficient, number.magnitude );

        public static implicit operator double ( LargeNumber number )
        {
            if ( number.magnitude == 0 )
                return number.coefficient;

            if ( number.coefficient == 0 )
                return 0d;

            return Math.Pow ( 1000, number.magnitude ) * number.coefficient;
        }


        [MethodImpl ( MethodImplOptions.AggressiveInlining )]
        private static LargeNumber Fix ( double c, int m )
        {
            // Check to see if the new value has jumped an order of magnitude.
            var absCoefficient = c < 0 ? -c : c;

            if ( absCoefficient == 0 )
                return new LargeNumber ( 0d, 0 );

            while ( absCoefficient < 1d )
            {
                --m;
                c *= 1000;
                absCoefficient *= 1000;
            }

            while ( absCoefficient >= 1000d )
            {
                ++m;
                c /= 1000;
                absCoefficient /= 1000;
            }
            return new LargeNumber ( c, m );
        }


        private static void Fix ( ref double c, ref int m )
        {
            // Check to see if the new value has jumped an order of magnitude.
            var absCoefficient = c < 0 ? -c : c;

            if ( absCoefficient == 0 )
                return;


            // e.g. c:0.00999, m:1 -> c:9.99, m:0.
            while ( absCoefficient < 0.01d )
            {
                --m;
                c *= 1000;
                absCoefficient *= 1000;
            }

            while ( absCoefficient >= 1000d )
            {
                ++m;
                c /= 1000;
                absCoefficient /= 1000;
            }
        }
    }
}
