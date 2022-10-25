// The following code runs operations against different large numbers.
// Demonstrated below is the ability to set and read values through the Inspector,
// as well as addition, multiplication and division operations.
// The demo also goes through the ways you can interact with the values and read the
// value components such as the coefficient and magnitude.

using UnityEngine;
using UnityEngine.UI;

using Debug = UnityEngine.Debug;

// Include this namespace to enable Large Numbers.
using LargeNumbers;


public class LargeNumberTestBehaviour : MonoBehaviour
{
    // The Large Number values can be serialised by Unity.
    [Header ( "Starting Values" )]
    public LargeNumber largeNumber = new LargeNumber ( 123.456, 789 );
    public ScientificNotation scientificNotation = new ScientificNotation ( 9.87654, 321 );
    public AlphabeticNotation alphabeticNotation = new AlphabeticNotation ( 987.654, 0 );

    [ Space ( 10 )]
    public int iterations = 40;

    [Space(10)]
    [Header("UI Text Elements")]
    public Text IterationsText;
    public Text StartLargeNumberText;
    public Text LargeNumberText;
    public Text StartScientificNotationText;
    public Text ScientificNotationText;
    public Text StartAlphabeticNotationText;
    public Text AlphabeticNotationText;

    private float timer = 0;
    private int iterationCount = 0;


    void Start ( )
    {
        {
            // Let's write out to the console the values that are held and viewed in the Inspector.

            // Calling ToString() is the easiest way to visualise the large number value.
            // It may not be the most efficient though, depending on your UI needs.
            var s = "1. Printing out the values from the Inspector. (Click here to see the results)\n\n";
            s += $"Printing the deserialised LargeNumber value as a string : {largeNumber}.\n" +
                $"  coefficient : {largeNumber.coefficient}. \n  magnitude : {largeNumber.magnitude}. \n" +
                $"  large number name : {LargeNumber.GetLargeNumberName ( largeNumber.magnitude )}\n\n";

            s += $"Printing the deserialised ScientificNotation value as a string : {scientificNotation}.\n  {scientificNotation.ToValuesString ( )}.\n\n";
            s += $"Printing the deserialised AlphabeticNotation value as a string : {alphabeticNotation}.\n  {alphabeticNotation.ToValuesString ( )}.\n\n";

            Debug.Log ( s );
        }


        {
            // Now we can run some mathematic operations on some new values.

            // We set up some simple Double types here for testing.
            var d1 = 1d;
            var d2 = 111_000_000_000d;

            // We create two LargeNumber items from the above double values. This shows one way to create a new LargeNumber.
            var l1 = new LargeNumber ( d1 );
            var l2 = new LargeNumber ( d2 );

            // We can supress the warning that "l3" is never used. This is not required at all, but it looks better in the editor.
#pragma warning disable CS0219
            var l3 = new LargeNumber ( ); // we can also just create a new default LargeNumber with a value of 0 (coefficient and magnitude).
#pragma warning restore CS0219  

            // We create two ScientificNotation items from the above double values.
            var s1 = new ScientificNotation ( d1 );
            var s2 = new ScientificNotation ( d2 );

            var a1 = new AlphabeticNotation ( d1 );
            var a2 = new AlphabeticNotation ( d2 );


            // The following two loops are just testing some multiplication and division of LargeNumber and ScientificNotation values.
            // At the end of the following two loops, the final results should be very close to the original. Unfortunately due to floating
            // point drift, there might be very small differences in the final number, but that's the nature of FPNs.
            var s = "2. Multiplying large numbers by themselves. (Click here to see the results)\n\n";
            for ( int n = 0; n < 5; n++ )
            {
                s += $"[{n}] Operation {d1} * {d2} = ";
                d1 *= d2; // multiply two double values
                l1 *= l2; // multiply two LarneNumber values
                s1 *= s2; // multiply two ScientificNotation values
                a1 *= a2; // multiply two AlphabeticNotation values

                // Now format the results to display in the Console window.
                s += $"{d1}.\n";
                s += $" LargeNumber = \t\t{l1}. {l1.ToValuesString ( )} double = {( double ) l1}\n";
                s += $" ScientificNotation = \t\t{s1}. {s1.ToValuesString ( )} double = {( double ) s1}\n";
                s += $" AlphabeticNotation = \t{a1}. {a1.ToValuesString ( )} double = {( double ) a1}\n\n";
            }
            Debug.Log ( s );

            s = "3. Divide the numbers to end up with the start values, where we MIGHT see small the floating point drift.\n(Click here to see the results)\n\n";
            for ( int n = 0; n < 5; n++ )
            {
                s += $"[{n}]\n Standard Double Operation {d1} / {d2} = ";
                d1 /= d2; // divide two double values
                l1 /= l2; // divide two LarneNumber values
                s1 /= s2; // divide two ScientificNotation values
                a1 /= a2; // divide two AlphabeticNotation values

                // Now format the results to display in the Console window.
                s += $"{d1}.\n";
                s += $" LargeNumber = \t\t{l1}. {l1.ToValuesString ( )} double = {( double ) l1}\n";
                s += $" ScientificNotation = \t\t{s1}. {s1.ToValuesString ( )} double = {( double ) s1}\n";
                s += $" AlphabeticNotation = \t{a1}. {a1.ToValuesString ( )} double = {( double ) a1}\n\n";
            }
            Debug.Log ( s );
        }


        {
            // The following code demonstrates implicitly casting from one large number to another.
            string s = "4. Casting values from one to another. (Click here to see the results)\n\n";

            LargeNumber l1 = new LargeNumber ( 123.456, 20 ); // = 1.23456 x 10^6
            ScientificNotation s1 = new ScientificNotation ( 123.456, 60 ); // = 1.23456 x 10^6
            AlphabeticNotation a1 = new AlphabeticNotation ( 123.456, 20 ); // = 1.23456 x 10^6

            LargeNumber l2 = s1; // implicitly cast a ScientificNotation to a LargeNumber
            ScientificNotation s2 = a1; // implicitly cast a AlphabeticNotation to a ScientificNotation
            AlphabeticNotation a2 = l1; // implicitly cast a LargeNumber to a AlphabeticNotation

            var l3 = ( LargeNumber ) a2;// explicitly cast a AlphabeticNotation to a LargeNumber
            var s3 = ( ScientificNotation ) l2; // explicitly cast a LargeNumber to a ScientificNotation
            var a3 = ( AlphabeticNotation ) s2; // explicitly cast a ScientificNotation to a AlphabeticNotation

            s += $"ScientificNotation : {s1} {s1.ToValuesString ( )} -> LargeNumber : {l2} {l2.ToValuesString ( )} \n" +
                $" -> ScientificNotation : {s3} {s3.ToValuesString ( )}\n\n";

            s += $"AlphabeticNotation : {a1} {a1.ToValuesString ( )} -> ScientificNotation : {s2} {s2.ToValuesString ( )} \n" +
                $" -> AlphabeticNotation : {a3} {a3.ToValuesString ( )}\n\n";

            s += $"LargeNumber : {l1} {l1.ToValuesString ( )} -> AlphabeticNotation : {a2} {a2.ToValuesString ( )} \n" +
                $" -> LargeNumber : {l3} {l3.ToValuesString ( )}\n\n";

            Debug.Log ( s );
        }

        {
            string s = "5. Comparing some LargeNumber values against each other and zero. (Click here to see the results)\n\n";
            var a = new LargeNumber ( 0.1, 105 );
            var b = new LargeNumber ( 20, 105 );
            var c = new LargeNumber ( 0.1, 106 );

            s += $"{a} < {b} = {a < b}\n";
            s += $"{a} < {c} = {a < c}\n";
            s += $"{b} < {c} = {b < c}\n";

            s += $"{a} < 0 = {a < LargeNumber.zero}\n";
            s += $"{b} > 0 = {b > LargeNumber.zero}\n";
            s += $"{c} == 0 = {c == LargeNumber.zero}\n";

            var d = new LargeNumber ( -0.1, -333 );
            s += $"{d} < 0 = {d < LargeNumber.zero}\n";
            Debug.Log ( s );
        }

        {
            string [ ] tests = { "M", "T", "a", "z", "aa", "az", "ba", "zz", "aaa", "xyz", "zzz", "aza", "azb", "aaaa", "zzzzzz", "aaaaaaa", "aazaa", "aazzz", "abaaa", "zaaaaa", "fxshrxr", "fxshrxs", "fxshrxt", "Kᵗʰ", "aᵗʰ", "bᵗʰ", "fxshrxsᵗʰ", "fxshrxtᵗʰ" };
            string s = "6. Getting some AlphabeticNotation magnitudes from a string (and back again). (Click here to see the results)\n\n";
            for ( int i = 0, count = tests.Length; i < count; ++i )
            {
                if ( !AlphabeticNotation.GetMagnitudeFromAlphabeticName ( tests [ i ], out var mag ) )
                    s += $"  6.{i} Could not get magnitude from '{tests [ i ]}' [{mag}]\n";
                else s += $"  6.{i} '{tests [ i ]}' => {mag} => {AlphabeticNotation.GetAlphabeticMagnitudeName(mag)}\n";
            }

            Debug.Log ( s );
        }

        {
            string [ ] tests = { "123", "1234567890", "123.456 abc", "-123,456xyz", "98.76 Kᵗʰ", ".1B", "abcd", "1.23zzz", "1.23aaaa", "1.23yzzz", "1.23zaaa", "9.876 fxshrxs" };
            string s = "7. Getting some AlphabeticNotation items from a string. (Click here to see the results)\n\n";
            for ( int i = 0, count = tests.Length; i < count; ++i )
            {
                if ( !AlphabeticNotation.GetAlphabeticNotationFromString ( tests [ i ], out var alphabeticNotation ) )
                    s += $"  7.{i} Could not get AlphabeticNotation from '{tests [ i ]}'\n";
                else s += $"  7.{i} '{tests [ i ]}' => {alphabeticNotation} {alphabeticNotation.ToValuesString()}\n";
            }

            Debug.Log ( s );
        }

        {
            string s = "8. Comparing large and small numbers (Click here to see the results)\n\n";
            var large = new AlphabeticNotation ( 123, 1234567890 );
            var small = new AlphabeticNotation ( 123, 1 );
            s += $"small {small.ToValuesString ( )}. large {large.ToValuesString ( )}\n";
            s += $"large <= small {large <= small}.\nsmall <= large {small <= large}\n";
            Debug.Log ( s );
        }

        Debug.Log ( " ... Now on to the GUI iteration part of the demonstration..\n\n" );
        IterationsText.text = $"Iteration : 0/{iterations}.";
        StartLargeNumberText.text = largeNumber.ToString ( );
        StartScientificNotationText.text = scientificNotation.ToString ( );
        StartAlphabeticNotationText.text = alphabeticNotation.ToString ( );
    }


    /// <summary>
    /// The Update() method here will drive the GUI part of the demonstration. It will run the number of iterations as specified in the Inspector.
    /// </summary>
    private void Update ( )
    {
        timer += Time.deltaTime;
        if ( timer > 0.5f && iterationCount < iterations )
        {
            timer -= 0.5f;
            iterationCount++;

            var output = $"Iteration : {iterationCount}/{iterations}.";
            if ( iterationCount == iterations )
                output += " Finished.";
            IterationsText.text = output;

            // You'll also notice that because we're working with the public values, the Inspector will also show the modified values during Play.
            // This only occurs during runtime, so when the editor stops playing, the values are returned to the original values.

            largeNumber *= 123.456; // multiply a LargeNumber value by a double.
            scientificNotation *= 123.456; // multiply a ScientificNotation value by double.
            alphabeticNotation *= 123.456; // multiply a AlphabeticNotation value by double.

            LargeNumberText.text = largeNumber.ToString ( );
            ScientificNotationText.text = scientificNotation.ToString ( );
            AlphabeticNotationText.text = alphabeticNotation.ToString ( );

            if ( iterationCount == iterations )
            {
                string finalOutput = $"Finish. Final Values as Standard Double values. (Click here to see the results) \n\n" +
                    $"largeNumber \t\t{largeNumber.Standard ( )}.\n" +
                    $"scientificNotation \t\t{scientificNotation.Standard ( )}.\n" +
                    $"alphabeticNotation \t {alphabeticNotation.Standard ( )}";
                Debug.Log ( finalOutput );
            }
        }
    }
}
