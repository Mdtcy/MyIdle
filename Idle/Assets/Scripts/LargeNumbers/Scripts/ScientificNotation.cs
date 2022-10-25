// ----------------------------------------------------------------------------
// LargeNumbers.ScientificNotation
// (c) Milan Egon Votrubec
//
// For documentation, please see the included "Large Numbers Library Welcome.pdf" file.
//
// For example usage, please see the "largenumbers.unity" example scene.
//
// Please see the "license.txt" file for licensing.
// ----------------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace LargeNumbers
{
    /// <summary>
    /// A Scientific Notation based number.
    /// For more details, visit https://en.wikipedia.org/wiki/Scientificnotation.
    /// </summary>
#if UNITY_EDITOR || UNITY_STANDALONE
    [Serializable]
#endif
    public struct ScientificNotation : IEquatable<ScientificNotation>
    {
        // ---------------------------------------------------------------------------- Static Variables and Methods

        private static StringBuilder sb = new StringBuilder ( 16 );

        // This list helps reduce Maths.Pow calls, but also idicates the precision of mathematic operations. Values that are deemed too far different in scale are ignored.
        // The largest long value is 9,223,372,036,854,775,807
        private static readonly long [ ] Powers = {
            1,
            10,
            100,
            1_000,
            10_000,
            100_000,
            1_000_000,
            10_000_000,
            100_000_000,
            1_000_000_000,
            10_000_000_000,
            100_000_000_000,
            1_000_000_000_000,
            10_000_000_000_000,
            100_000_000_000_000,
            1_000_000_000_000_000,
            10_000_000_000_000_000,
            100_000_000_000_000_000
        };


        // ---------------------------------------------------------------------------- Properties
        /// <summary>
        /// The coefficient is the value, or the number, that preceeds the exponent part of the scientific notation ( coefficient x 10^magnitude ).
        /// </summary>
        public double coefficient;

        /// <summary>
        /// Magnitude refers to the exponent of the scientific notation ( coefficient x 10^magnitude ).
        /// </summary>
        public int magnitude;


        // ---------------------------------------------------------------------------- Methods
        /// <summary>
        /// Create a ScientificNotation number from a real double value.
        /// </summary>
        /// <param name="real">A double type value to be converted into Scientific Notation.</param>
        public ScientificNotation ( double value )
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
        /// Create a ScientificNotation number from a coefficient and a magnitude.
        /// </summary>
        /// <param name="coefficient">A double type representing the Coefficient.</param>
        /// <param name="magnitude">An integer representing the Exponent of the scientific notation.</param>
        public ScientificNotation ( double coefficient, int magnitude )
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
        /// Returns the Scientific Notation value as a "standard" double data type.
        /// </summary>
        /// <returns>The double type representation. Be aware the the result might be +- Infinity if the value overflows.</returns>
        /// <remarks>Not a particularly useful method.</remarks>
        public double Standard ( )
        {
            if ( magnitude == 0 )
                return coefficient;

            if ( coefficient == 0 )
                return 0d;

            return Math.Pow ( 10, magnitude ) * coefficient;
        }


        /// <summary>
        /// Because the string representation of this number could be displayed in so many different ways, this method is used
        /// mainly to get the most basic representation of a scientific notation number. Think debugging purposes.
        /// Type-Conversion (changing a numeric value to a string) will pretty much guarantee the production of some garbage.
        /// </summary>
        public override string ToString ( )
        {
            sb.Clear ( );
            var absMagnitude = magnitude < 0 ? -magnitude : magnitude;
            if ( absMagnitude < 3 )
            {
                if ( magnitude > 0 )
                    sb.ConvertAndAppend ( coefficient * Powers [ absMagnitude ], 3, false );
                else
                    sb.ConvertAndAppend ( coefficient / Powers [ absMagnitude ], 3, false );
            }
            else
                sb.ConvertAndAppend ( coefficient, 3, false ).Append ( " x 10^" ).ConvertAndAppend ( magnitude );
            return sb.ToString ( );
        }


        public static ScientificNotation zero => new ScientificNotation ( );
        public bool isZero => coefficient == 0;

        // ---------------------------------------------------------------------------- Compare, Equatable

        /// <summary>
        /// Checks to see how this numbers compares to another numbers.
        /// </summary>
        /// <param name="other">The other numbers.</param>
        /// <returns>0 if the two numbers are the same*. -1 if this numbers smaller. 1 if this numbers is larger.</returns>
        public int CompareTo ( ScientificNotation other )
        {
            // Difference in coefficient signs. Instant giveaway.
            if ( other.coefficient < 0 && coefficient > 0 ) return 1;
            if ( other.coefficient > 0 && coefficient < 0 ) return -1;

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
        /// <returns>True if the two numbers are the same to within three decimal places.</returns>
        public bool Equals ( ScientificNotation other )
        {
            var magnitudeDifference = other.magnitude - magnitude;
            // Now performs an equality check with numbers up to 1 order of magnitude difference.
            if ( magnitudeDifference < -1 || magnitudeDifference > 1 ) return false;
            //if ( other.magnitude != magnitude ) return false;
            var c1 = ( int ) ( coefficient * 1_000 * ( magnitudeDifference == -1 ? 10 : 1 ) );
            var c2 = ( int ) ( other.coefficient * 1_000 * ( magnitudeDifference == 1 ? 10 : 1 ) );
            return c1 == c2;
        }


        public override bool Equals ( Object obj )
        {
            return obj is ScientificNotation other && Equals ( other );
        }


        public override int GetHashCode ( )
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + coefficient.GetHashCode ( );
                return hash * 23 + magnitude.GetHashCode ( );
            }
        }


        // ---------------------------------------------------------------------------- Operator Overloading
        // Note : In most cases, using the methods supplied for arithmatic operations is significantly faster.
        // BUT... those performance gains are only noticable when operation is performed 10's of millions of times.
        public static ScientificNotation operator - ( ScientificNotation a, ScientificNotation b )
        {
            var magnitudeDifference = a.magnitude - b.magnitude;
            var absmagnitudeDifference = magnitudeDifference < 0 ? -magnitudeDifference : magnitudeDifference;

            var returnCoefficient = a.coefficient;
            var returnMagnitude = a.magnitude;

            if ( magnitudeDifference == 0 )
            {
                // The magnitudes are the same, so we can just add the values together.
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
            return new ScientificNotation ( returnCoefficient, returnMagnitude );
        }


        public static ScientificNotation operator + ( ScientificNotation a, ScientificNotation b )
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
                        returnCoefficient = ( a.coefficient / Powers [ absmagnitudeDifference ] ) + b.coefficient;
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
            return new ScientificNotation ( returnCoefficient, returnMagnitude );
        }

        public static ScientificNotation operator + ( ScientificNotation a, double b ) => a + new ScientificNotation ( b );
        public static ScientificNotation operator + ( double a, ScientificNotation b ) => new ScientificNotation ( a ) + b;
        public static ScientificNotation operator - ( ScientificNotation a, double b ) => a - new ScientificNotation ( b );
        public static ScientificNotation operator - ( double a, ScientificNotation b ) => new ScientificNotation ( a ) - b;
        public static ScientificNotation operator * ( ScientificNotation a, ScientificNotation b ) => new ScientificNotation ( a.coefficient * b.coefficient, a.magnitude + b.magnitude );
        public static ScientificNotation operator * ( ScientificNotation a, double b ) => new ScientificNotation ( a.coefficient * b, a.magnitude );
        public static ScientificNotation operator * ( double a, ScientificNotation b ) => new ScientificNotation ( a * b.coefficient, b.magnitude );
        public static ScientificNotation operator / ( ScientificNotation a, ScientificNotation b ) => new ScientificNotation ( a.coefficient / b.coefficient, a.magnitude - b.magnitude );
        public static ScientificNotation operator / ( ScientificNotation a, double b ) => new ScientificNotation ( a.coefficient / b, a.magnitude );
        public static ScientificNotation operator / ( double a, ScientificNotation b ) => new ScientificNotation ( a / b.coefficient, b.magnitude );

        public static bool operator < ( ScientificNotation a, ScientificNotation b ) { return a.CompareTo ( b ) == -1; }
        public static bool operator <= ( ScientificNotation a, ScientificNotation b ) { return a.CompareTo ( b ) <= 0; }
        public static bool operator > ( ScientificNotation a, ScientificNotation b ) { return a.CompareTo ( b ) == 1; }
        public static bool operator >= ( ScientificNotation a, ScientificNotation b ) { return a.CompareTo ( b ) >= 0; }


        public static bool operator == ( ScientificNotation a, ScientificNotation b )
        {
            return a.Equals ( b );
        }


        public static bool operator != ( ScientificNotation a, ScientificNotation b )
        {
            return !a.Equals ( b );
        }




        private static void Fix ( ref double c, ref int m )
        {
            // Check to see if the new value has jumped an order of magnitude.
            var absCoefficient = c < 0 ? -c : c;

            if ( absCoefficient == 0 )
                return;

            while ( absCoefficient < 1d )
            {
                --m;
                c *= 10;
                absCoefficient *= 10;
            }

            while ( absCoefficient >= 10d )
            {
                ++m;
                c /= 10;
                absCoefficient /= 10;
            }
        }

        public string ToValuesString ( ) => $"(c:{coefficient} m:{magnitude})";

        public static implicit operator LargeNumber ( ScientificNotation number )
        {
            var c = number.coefficient * Powers [ number.magnitude % 3 ];
            var m = number.magnitude / 3;

            return new LargeNumber ( c, m );
        }

        public static implicit operator AlphabeticNotation ( ScientificNotation number )
        {
            var c = number.coefficient * Powers [ number.magnitude % 3 ];
            var m = number.magnitude / 3;

            return new AlphabeticNotation ( c, m );
        }

        public static implicit operator double ( ScientificNotation number )
        {
            if ( number.magnitude == 0 )
                return number.coefficient;

            if ( number.coefficient == 0 )
                return 0d;

            return Math.Pow ( 10, number.magnitude ) * number.coefficient;
        }
    }
}