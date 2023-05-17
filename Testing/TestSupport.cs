// TestSupport.cs (zu Zulassungsaufgabe 23S)
// statische Klasse zur Unterstützung von Tests
// nur für Konsolenanwendungen

#nullable disable

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Testing
{
    public static class TestSupport
    {
        /// <summary>
        /// true: bei Fehler wird Ausnahme geworfen
        /// false: bei Fehler wird der Fehlerzähler erhöht
        /// </summary>
        public static bool ThrowException { get; set; } = false;

        /// <summary>
        /// Anzahl der gefundenen Fehler
        /// </summary>
        public static int Errors { get; private set; } = 0;

        /// <summary>
        /// Farbe für fehlerhafte Ausgaben
        /// </summary>
        public const ConsoleColor BadColor = ConsoleColor.DarkRed;

        /// <summary>
        /// Farbe für korrekte Ausgaben
        /// </summary>
        public const ConsoleColor GoodColor = ConsoleColor.DarkGreen;

        /// <summary>
        /// farbiger Zeilenausdruck auf Konsole
        /// </summary>
        /// <param name="text">auszugebender Text</param>
        /// <param name="color">Hintergrundfarbe des Textes</param>
        public static void ColorPrint(string text, ConsoleColor color)
        {
            if (String.IsNullOrEmpty(text))
            {
                ErrorMessage("ColorPrint: fehlende Nachricht");
                return;
            }

            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
            if (color == BadColor)
            {
                Errors++;
                if (ThrowException)
                    throw new Exception();
            }
        }

        /// <summary>
        /// Methode zum Equals-Vergleich zweier Objekte mit farblicher Rückmeldung
        /// </summary>
        /// <param name="obj">erstes Objekt</param>
        /// <param name="comp">zweites Objekt</param>
        /// <param name="trueColor">Farbe bei Übereinstimmung</param>
        /// <param name="falseColor">Farbe bei Nichtübereinstimmung</param>
        public static void CompareAndPrint(object obj, object comp, ConsoleColor trueColor = GoodColor, ConsoleColor falseColor = BadColor)
        {
            if (obj == null || comp == null)
            {
                ErrorMessage("CompareAndPrint: fehlende Parameter");
                return;
            }
            ColorPrint(">" + obj.ToString() + "< vs. >" + comp.ToString() + "<", obj.Equals(comp) ? trueColor : falseColor);
        }

        /// <summary>
        /// Methode zum Vergleich zweier double-Zahlen
        /// </summary>
        /// <param name="d1">erste Zahl</param>
        /// <param name="d2">zweite Zahl</param>
        /// <param name="trueColor">Farbe bei Übereinstimmung</param>
        /// <param name="falseColor">Farbe bei Nichtübereinstimmung</param>
        /// <remarks>erlaubte Abweichung muss kleiner 10^-10 sein</remarks>
        public static void CompareAndPrintDouble(double d1, double d2, ConsoleColor trueColor = GoodColor, ConsoleColor falseColor = BadColor)
        {
            ColorPrint(">" + d1.ToString() + "< vs. >" + d2.ToString() + "<", Math.Abs(d1 - d2) < 1E-10 ? trueColor : falseColor);
        }

        /// <summary>
        /// Methode zur Prüfung auf null
        /// </summary>
        /// <param name="obj">zu prüfende Referenz</param>
        /// <param name="trueColor">Farbe bei Übereinstimmung (Referenz ist null)</param>
        /// <param name="falseColor">Farbe bei Nichtübereinstimmung (Referenz ungleich null)</param>
        public static void IsNull(object obj, ConsoleColor trueColor = GoodColor, ConsoleColor falseColor = BadColor)
        {
            if (obj == null)
                ColorPrint("NULL", trueColor);
            else
                ColorPrint(obj.ToString(), falseColor);
        }

        /// <summary>
        /// Methode zum Test der überschriebenen Methoden ToString(), GetHashCode() und Equals(),
        /// bei Werttypen auch die Operatoren == und !=
        /// </summary>
        /// <typeparam name="T">Klasse oder Struct, deren/dessen Methoden getestet werden sollen</typeparam>
        /// <param name="l">Liste von Objekten derselben Klasse</param>
        /// <remarks>Die ersten beiden Objekte der Liste müssen wertgleich sein, die übrigen wertverschieden von den ersten beiden.</remarks>
        public static void ObjectTest<T>(T[] l)
        {
            if (l == null || l.Length < 3)
            {
                ErrorMessage("ObjectTest: fehlendes oder zu kurzes Array");
                return;
            }

            Console.WriteLine("\n----------\n-- ToString");

            foreach (object o in l)
                Console.WriteLine(o.ToString());

            Console.WriteLine("\n-- GetHashCode");

            int compHashCode = l[0].GetHashCode();
            Console.WriteLine(compHashCode);
            CompareAndPrint(l[1].GetHashCode(), compHashCode);
            for (int i = 2; i < l.Length; i++)
                CompareAndPrint(l[i].GetHashCode(), compHashCode, BadColor, GoodColor);

            Console.WriteLine("\n-- Equals");

            CompareAndPrint(l[1], l[0]);
            for (int i = 2; i < l.Length; i++)
                CompareAndPrint(l[i], l[0], BadColor, GoodColor);
            CompareAndPrint(l[0], new Object(), BadColor, GoodColor);

            // Überprüfung der Operatoren == und != für Werttypen
            Type type = typeof(T);
            if (type.IsValueType)
            {
                Console.WriteLine("\n-- == / !=");

                MethodInfo opEquality = type.GetMethod("op_Equality");
                MethodInfo opInequality = type.GetMethod("op_Inequality");
                if (opEquality == null || opInequality == null)
                {
                    ErrorMessage("Operatoren == oder != fehlen!");
                    return;
                }
                CompareAndPrint(opEquality.Invoke(null, new object[] { l[1], l[0] }), true);
                for (int i = 2; i < l.Length; i++)
                    CompareAndPrint(opInequality.Invoke(null, new object[] { l[i], l[0] }), true);
            }
        }

        /// <summary>
        /// Methode zum Testen der Methode CompareTo()
        /// </summary>
        /// <param name='l'>
        /// sortierte Liste von Objekten derselben Klasse
        /// </param>
        /// <remarks>
        /// Die ersten beiden Objekte der Liste müssen wertgleich sein, die übrigen aufsteigend sortiert.
        /// Die Methode darf nur für Klassen aufgerufen werden, die IComparable implementieren.
        /// </remarks>
        public static void SortedTest(IComparable[] l)
        {
            if (l == null || l.Length < 3)
            {
                ErrorMessage("SortedTest: fehlendes oder zu kurzes Array");
                return;
            }

            Console.WriteLine("\n----------\n");

            CompareAndPrint(Math.Sign((l[1]).CompareTo(l[0])), 0, GoodColor, BadColor);
            for (int i = 2; i < l.Length; i++)
            {
                CompareAndPrint(Math.Sign((l[i]).CompareTo(l[0])), 1, GoodColor, BadColor);
                CompareAndPrint(Math.Sign((l[0]).CompareTo(l[i])), -1, GoodColor, BadColor);
            }
        }

        /// <summary>
        /// Test der Plausibilitätsprüfungen
        /// </summary>
        /// <param name="action">Aktion, die eine Ausnahme auslösen soll</param>
        public static void ProvokeException(Action action)
        {
            if (action == null)
            {
                ErrorMessage("ProvokeException: fehlende Aktion");
                return;
            }

            try
            {
                action();
            }
            catch
            {
                ColorPrint("Exception OK", GoodColor);
                return;
            }
            ColorPrint("Exception FAIL", BadColor);
        }

        /// <summary>
        /// Prüfung auf Nicht-Änderbarkeit der Eigenschaften einer Klasse
        /// </summary>
        /// <typeparam name="T">zu prüfende Klasse</typeparam>
        public static void NonwriteableProperties<T>()
        {
            foreach (PropertyInfo pi in typeof(T).GetProperties())
            {
                Console.Write(pi.Name + " änderbar: ");
                CompareAndPrint(pi.CanWrite, false);
            }
        }

        /// <summary>
        /// Prüfung, ob eine Methode in einer Klasse neu deklariert wurde
        /// </summary>
        /// <typeparam name="T">zu prüfende Klasse</typeparam>
        /// <param name="name">zu prüfende Methode</param>
        /// <exception cref="ArgumentNullException">Ausnahme bei fehlendem Methodenbezeichner</exception>
        public static void TestForNewMethod<T>(string name)
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));
            Console.Write($"{name,15}: ");
            MethodInfo mi = typeof(T).GetMethod(name);
            if (mi == null)
                ErrorMessage("Methode {name} existiert nicht!");
            else
                CompareAndPrint(mi.DeclaringType, typeof(T));
        }

        /// <summary>
        /// Prüfung, ob die drei aus Object ererbten überschreibbaren Methoden auch wirklich überschrieben sind
        /// </summary>
        /// <typeparam name="T">zu prüfende Klasse</typeparam>
        public static void TestForOverriddenMethodsFromObject<T>()
        {
            Console.WriteLine();
            TestForNewMethod<T>("ToString");
            TestForNewMethod<T>("Equals");
            TestForNewMethod<T>("GetHashCode");
        }

        /// <summary>
        /// Ausgabe einer Fehlermeldung
        /// </summary>
        /// <param name="message">Text der Fehlermeldung</param>
        public static void ErrorMessage(string message = "fehlende Fehlermeldung ;)") => ColorPrint(message, BadColor);

        /// <summary>
        /// Ausgabe der Quellcode-Datei und Zeilennummer
        /// </summary>
        /// <param name="file">optionaler Parameter Dateiname (wird automatisch bestimmt)</param>
        /// <param name="lineNumber">optionaler Parameter Zeilennummer (wird automatisch bestimmt)</param>
        public static void PL([CallerFilePath] string file = null, [CallerLineNumber] int lineNumber = 0)
            => Console.Write("[{0}:{1:000}] ", Regex.Replace(file, @"^.*\\", ""), lineNumber);

        /// <summary>
        /// Ausgabe des Fehlerzählers
        /// </summary>
        public static void PrintResult()
        {
            if (Errors > 0)
                ColorPrint("Fehler: " + Errors.ToString(), BadColor);
            else
                ColorPrint("keine Fehler", GoodColor);

        }
    }
}
