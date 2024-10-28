using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace TextConversion
{
    public partial class Unicode : Form
    {
        private static Random random = new Random();

        // Dictionary of common confusable characters with their names -- Latin Is Normally The Default So We Just Ensure Latin If It Doesn't Have A Look Alike
        private static readonly Dictionary<char, List<(string Character, string Name)>> confusables = new Dictionary<char, List<(string, string)>>
        {
            { 'a', new List<(string, string)> { ("a", "Latin 'a'"), ("а", "Cyrillic 'а'")} },
            { 'A', new List<(string, string)> { ("A", "Latin 'A'"), ("А", "Cyrillic 'А'"), ("Α", "Greek 'A'")} },
            { 'b', new List<(string, string)> { ("b", "Latin 'b'"), ("Ь", "Cyrillic 'Ь'")} },
            { 'B', new List<(string, string)> { ("B", "Latin 'B'"), ("В", "Cyrillic 'В'"), ("Β", "Greek 'Β'") } },
            { 'c', new List<(string, string)> { ("c", "Latin 'c'"), ("с", "Cyrillic 'с'"), ("ⅽ", "Roman 'ⅽ'") } },
            { 'C', new List<(string, string)> { ("C", "Latin 'C'"), ("С", "Cyrillic 'С'"), ("Ϲ", "Greek 'Ϲ'"), ("Ⅽ", "Roman 'Ⅽ'") } },
            { 'd', new List<(string, string)> { ("d", "Latin 'd'"), ("ԁ", "Cyrillic 'ԁ'")} },
            { 'D', new List<(string, string)> { ("D", "Latin 'D'"), ("Ⅾ", "Roman 'Ⅾ'")} },
            { 'e', new List<(string, string)> { ("e", "Latin 'e'"), ("е", "Cyrillic 'е'")} },
            { 'E', new List<(string, string)> { ("E", "Latin 'E'"), ("Е", "Cyrillic 'Е'"), ("Ε", "Greek 'Ε'") } },
            { 'f', new List<(string, string)> { ("f", "Latin 'f'")} },
            { 'F', new List<(string, string)> { ("F", "Latin 'F'")} },
            { 'g', new List<(string, string)> { ("g", "Latin 'g'"), ("ց", "Armenian 'ց'") } },
            { 'G', new List<(string, string)> { ("G", "Latin 'G'"), ("Ԍ", "Cyrillic 'Ԍ'")} },
            { 'h', new List<(string, string)> { ("h", "Latin 'h'"), ("һ", "Cyrillic 'һ'")} },
            { 'H', new List<(string, string)> { ("H", "Latin 'H'"), ("Н", "Cyrillic 'Н'"), ("Η", "Greek 'Η'")} },
            { 'i', new List<(string, string)> { ("i", "Latin 'i'"), ("і", "Cyrillic 'і'"), ("ⅰ", "Roman 'ⅰ'") } },
            { 'I', new List<(string, string)> { ("I", "Latin 'I'"), ("І", "Cyrillic 'І'"), ("Ι", "Greek 'Ι'") } },
            { 'j', new List<(string, string)> { ("j", "Latin 'j'"), ("ј", "Cyrillic 'ј'")} },
            { 'J', new List<(string, string)> { ("J", "Latin 'J'"), ("Ј", "Cyrillic 'Ј'")} },
            { 'k', new List<(string, string)> { ("k", "Latin 'k'")} },
            { 'K', new List<(string, string)> { ("K", "Latin 'K'"), ("К", "Cyrillic 'К'"), ("Κ", "Greek 'Κ'") } },
            { 'l', new List<(string, string)> { ("l", "Latin 'l'"), ("ӏ", "Cyrillic 'ӏ'") } },
            { 'L', new List<(string, string)> { ("L", "Latin 'L'")} },
            { 'm', new List<(string, string)> { ("m", "Latin 'm'"), ("м", "Cyrillic 'м'"), ("ⅿ", "Roman 'ⅿ'") } },
            { 'M', new List<(string, string)> { ("M", "Latin 'M'"), ("М", "Cyrillic 'М'"), ("Μ", "Greek 'Μ'") } },
            { 'n', new List<(string, string)> { ("n", "Latin 'n'"), ("ո", "Armenian 'ո'") } },
            { 'N', new List<(string, string)> { ("N", "Latin 'N'"), ("Ν", "Greek 'Ν'")} },
            { 'o', new List<(string, string)> { ("o", "Latin 'o'"), ("о", "Cyrillic 'о'"), ("ο", "Greek 'ο'")} },
            { 'O', new List<(string, string)> { ("O", "Latin 'O'"), ("Ο", "Greek 'Ο'"), ("О", "Cyrillic 'О'") } },
            { 'p', new List<(string, string)> { ("p", "Latin 'p'"), ("р", "Cyrillic 'р'")} },
            { 'P', new List<(string, string)> { ("P", "Latin 'P'"), ("Р", "Cyrillic 'Р'"), ("Ρ", "Greek 'Ρ'") } },
            { 'q', new List<(string, string)> { ("q", "Latin 'q'"), ("ԛ", "Cyrillic 'ԛ'")} },
            { 'Q', new List<(string, string)> { ("Q", "Latin 'Q'"), ("Ԛ", "Cyrillic 'Ԛ'")} },
            { 'r', new List<(string, string)> { ("r", "Latin 'r'")} },
            { 'R', new List<(string, string)> { ("R", "Latin 'R'")} },
            { 's', new List<(string, string)> { ("s", "Latin 's'"), ("ѕ", "Cyrillic 'ѕ'")} },
            { 'S', new List<(string, string)> { ("S", "Latin 'S'"), ("Ѕ", "Cyrillic 'Ѕ'")} },
            { 't', new List<(string, string)> { ("t", "Latin 't'")} },
            { 'T', new List<(string, string)> { ("T", "Latin 'T'"), ("Т", "Cyrillic 'Т'"), ("Τ", "Greek 'Τ'")} },
            { 'u', new List<(string, string)> { ("u", "Latin 'u'"), ("υ", "Greek 'υ'"), ("ս", "Armenian 'ս'") } },
            { 'U', new List<(string, string)> { ("U", "Latin 'U'"), ("Ս", "Armenian 'Ս'") } },
            { 'v', new List<(string, string)> { ("v", "Latin 'v'"), ("ν", "Greek 'ν'")} },
            { 'V', new List<(string, string)> { ("V", "Latin 'V'")} },
            { 'w', new List<(string, string)> { ("w", "Latin 'w'"), ("ԝ", "Cyrillic 'ԝ'")} },
            { 'W', new List<(string, string)> { ("W", "Latin 'W'")} },
            { 'x', new List<(string, string)> { ("x", "Latin 'x'"), ("х", "Cyrillic 'х'")} },
            { 'X', new List<(string, string)> { ("X", "Latin 'X'"), ("Χ", "Greek 'Χ'"), ("Х", "Cyrillic 'Х'") } },
            { 'y', new List<(string, string)> { ("y", "Latin 'y'"), ("у", "Cyrillic 'у'")} },
            { 'Y', new List<(string, string)> { ("Y", "Latin 'Y'"), ("Υ", "Greek 'Υ'"), ("Ү", "Cyrillic 'Ү'") } },
            { 'z', new List<(string, string)> { ("z", "Latin 'z'")} },
            { 'Z', new List<(string, string)> { ("Z", "Latin 'Z'"), ("Ζ", "Greek 'Ζ'")} },
        };



        // Greek Alphabet
        private static readonly Dictionary<char, List<(string Character, string Name)>> GreekAlphabet = new Dictionary<char, List<(string, string)>>
        {
            { 'a', new List<(string, string)> { ("α", "Greek 'α'") } },
            { 'A', new List<(string, string)> { ("Α", "Greek 'Α'") } },
            { 'b', new List<(string, string)> { ("β", "Greek 'β'") } },
            { 'B', new List<(string, string)> { ("Β", "Greek 'Β'") } },
            { 'g', new List<(string, string)> { ("γ", "Greek 'γ'") } },
            { 'G', new List<(string, string)> { ("Γ", "Greek 'Γ'") } },
            { 'd', new List<(string, string)> { ("δ", "Greek 'δ'") } },
            { 'D', new List<(string, string)> { ("Δ", "Greek 'Δ'") } },
            { 'e', new List<(string, string)> { ("ε", "Greek 'ε'") } },
            { 'E', new List<(string, string)> { ("Ε", "Greek 'Ε'") } },
            { 'z', new List<(string, string)> { ("ζ", "Greek 'ζ'") } },
            { 'Z', new List<(string, string)> { ("Ζ", "Greek 'Ζ'") } },
            { 'h', new List<(string, string)> { ("η", "Greek 'η'") } },
            { 'H', new List<(string, string)> { ("Η", "Greek 'Η'") } },
            { 'q', new List<(string, string)> { ("θ", "Greek 'θ'") } },
            { 'Q', new List<(string, string)> { ("Θ", "Greek 'Θ'") } },
            { 'i', new List<(string, string)> { ("ι", "Greek 'ι'") } },
            { 'I', new List<(string, string)> { ("Ι", "Greek 'Ι'") } },
            { 'k', new List<(string, string)> { ("κ", "Greek 'κ'") } },
            { 'K', new List<(string, string)> { ("Κ", "Greek 'Κ'") } },
            { 'l', new List<(string, string)> { ("λ", "Greek 'λ'") } },
            { 'L', new List<(string, string)> { ("Λ", "Greek 'Λ'") } },
            { 'm', new List<(string, string)> { ("μ", "Greek 'μ'") } },
            { 'M', new List<(string, string)> { ("Μ", "Greek 'Μ'") } },
            { 'n', new List<(string, string)> { ("ν", "Greek 'ν'") } },
            { 'N', new List<(string, string)> { ("Ν", "Greek 'Ν'") } },
            { 'x', new List<(string, string)> { ("ξ", "Greek 'ξ'") } },
            { 'X', new List<(string, string)> { ("Ξ", "Greek 'Ξ'") } },
            { 'o', new List<(string, string)> { ("ο", "Greek 'ο'") } },
            { 'O', new List<(string, string)> { ("Ο", "Greek 'Ο'") } },
            { 'p', new List<(string, string)> { ("π", "Greek 'π'") } },
            { 'P', new List<(string, string)> { ("Π", "Greek 'Π'") } },
            { 'r', new List<(string, string)> { ("ρ", "Greek 'ρ'") } },
            { 'R', new List<(string, string)> { ("Ρ", "Greek 'Ρ'") } },
            { 's', new List<(string, string)> { ("σ", "Greek 'σ'") } },
            { 'S', new List<(string, string)> { ("Σ", "Greek 'Σ'") } },
            { 't', new List<(string, string)> { ("τ", "Greek 'τ'") } },
            { 'T', new List<(string, string)> { ("Τ", "Greek 'Τ'") } },
            { 'u', new List<(string, string)> { ("υ", "Greek 'υ'") } },
            { 'U', new List<(string, string)> { ("Υ", "Greek 'Υ'") } },
            { 'f', new List<(string, string)> { ("φ", "Greek 'φ'") } },
            { 'F', new List<(string, string)> { ("Φ", "Greek 'Φ'") } },
            { 'c', new List<(string, string)> { ("χ", "Greek 'χ'") } },
            { 'C', new List<(string, string)> { ("Χ", "Greek 'Χ'") } },
            { 'y', new List<(string, string)> { ("ψ", "Greek 'ψ'") } },
            { 'Y', new List<(string, string)> { ("Ψ", "Greek 'Ψ'") } },
            { 'w', new List<(string, string)> { ("ω", "Greek 'ω'") } },
            { 'W', new List<(string, string)> { ("Ω", "Greek 'Ω'") } },
        };

        // Italic Characters
        private static readonly Dictionary<char, List<(string Character, string Name)>> ItalicAlphabet = new Dictionary<char, List<(string, string)>>
        {
            { 'a', new List<(string, string)> { ("𝑎", "Italic '𝑎'") } },
            { 'A', new List<(string, string)> { ("𝑨", "Italic 'A'") } },
            { 'b', new List<(string, string)> { ("𝑏", "Italic '𝑏'") } },
            { 'B', new List<(string, string)> { ("𝑩", "Italic 'B'") } },
            { 'c', new List<(string, string)> { ("𝑐", "Italic '𝑐'") } },
            { 'C', new List<(string, string)> { ("𝑪", "Italic 'C'") } },
            { 'd', new List<(string, string)> { ("𝑑", "Italic '𝑑'") } },
            { 'D', new List<(string, string)> { ("𝑫", "Italic 'D'") } },
            { 'e', new List<(string, string)> { ("𝑒", "Italic '𝑒'") } },
            { 'E', new List<(string, string)> { ("𝑬", "Italic 'E'") } },
            { 'f', new List<(string, string)> { ("𝑓", "Italic '𝑓'") } },
            { 'F', new List<(string, string)> { ("𝑭", "Italic 'F'") } },
            { 'g', new List<(string, string)> { ("𝑔", "Italic '𝑔'") } },
            { 'G', new List<(string, string)> { ("𝑮", "Italic 'G'") } },
            { 'h', new List<(string, string)> { ("𝑕", "Italic '𝑕'") } },
            { 'H', new List<(string, string)> { ("𝑯", "Italic 'H'") } },
            { 'i', new List<(string, string)> { ("𝑖", "Italic '𝑖'") } },
            { 'I', new List<(string, string)> { ("𝑰", "Italic 'I'") } },
            { 'j', new List<(string, string)> { ("𝑗", "Italic '𝑗'") } },
            { 'J', new List<(string, string)> { ("𝑱", "Italic 'J'") } },
            { 'k', new List<(string, string)> { ("𝑘", "Italic '𝑘'") } },
            { 'K', new List<(string, string)> { ("𝑲", "Italic 'K'") } },
            { 'l', new List<(string, string)> { ("𝑙", "Italic '𝑙'") } },
            { 'L', new List<(string, string)> { ("𝑳", "Italic 'L'") } },
            { 'm', new List<(string, string)> { ("𝑚", "Italic '𝑚'") } },
            { 'M', new List<(string, string)> { ("𝑴", "Italic 'M'") } },
            { 'n', new List<(string, string)> { ("𝑛", "Italic '𝑛'") } },
            { 'N', new List<(string, string)> { ("𝑵", "Italic 'N'") } },
            { 'o', new List<(string, string)> { ("𝑜", "Italic '𝑜'") } },
            { 'O', new List<(string, string)> { ("𝑶", "Italic 'O'") } },
            { 'p', new List<(string, string)> { ("𝑝", "Italic '𝑝'") } },
            { 'P', new List<(string, string)> { ("𝑷", "Italic 'P'") } },
            { 'q', new List<(string, string)> { ("𝑞", "Italic '𝑞'") } },
            { 'Q', new List<(string, string)> { ("𝑸", "Italic 'Q'") } },
            { 'r', new List<(string, string)> { ("𝑟", "Italic '𝑟'") } },
            { 'R', new List<(string, string)> { ("𝑹", "Italic 'R'") } },
            { 's', new List<(string, string)> { ("𝑠", "Italic '𝑠'") } },
            { 'S', new List<(string, string)> { ("𝑺", "Italic 'S'") } },
            { 't', new List<(string, string)> { ("𝑡", "Italic '𝑡'") } },
            { 'T', new List<(string, string)> { ("𝑻", "Italic 'T'") } },
            { 'u', new List<(string, string)> { ("𝑢", "Italic '𝑢'") } },
            { 'U', new List<(string, string)> { ("𝑼", "Italic 'U'") } },
            { 'v', new List<(string, string)> { ("𝑣", "Italic '𝑣'") } },
            { 'V', new List<(string, string)> { ("𝑽", "Italic 'V'") } },
            { 'w', new List<(string, string)> { ("𝑤", "Italic '𝑤'") } },
            { 'W', new List<(string, string)> { ("𝑾", "Italic 'W'") } },
            { 'x', new List<(string, string)> { ("𝑥", "Italic '𝑥'") } },
            { 'X', new List<(string, string)> { ("𝑿", "Italic 'X'") } },
            { 'y', new List<(string, string)> { ("𝑦", "Italic '𝑦'") } },
            { 'Y', new List<(string, string)> { ("𝑌", "Italic 'Y'") } },
            { 'z', new List<(string, string)> { ("𝑧", "Italic '𝑧'") } },
            { 'Z', new List<(string, string)> { ("𝑭", "Italic 'Z'") } },
        };


        // Bold Alphabet
        private static readonly Dictionary<char, List<(string Character, string Name)>> BoldAlphabet = new Dictionary<char, List<(string, string)>>
        {
            { 'a', new List<(string, string)> { ("𝐚", "Bold 'a'") } },
            { 'b', new List<(string, string)> { ("𝐛", "Bold 'b'") } },
            { 'c', new List<(string, string)> { ("𝐜", "Bold 'c'") } },
            { 'd', new List<(string, string)> { ("𝐝", "Bold 'd'") } },
            { 'e', new List<(string, string)> { ("𝐞", "Bold 'e'") } },
            { 'f', new List<(string, string)> { ("𝐟", "Bold 'f'") } },
            { 'g', new List<(string, string)> { ("𝐠", "Bold 'g'") } },
            { 'h', new List<(string, string)> { ("𝐡", "Bold 'h'") } },
            { 'i', new List<(string, string)> { ("𝐢", "Bold 'i'") } },
            { 'j', new List<(string, string)> { ("𝐣", "Bold 'j'") } },
            { 'k', new List<(string, string)> { ("𝐤", "Bold 'k'") } },
            { 'l', new List<(string, string)> { ("𝐥", "Bold 'l'") } },
            { 'm', new List<(string, string)> { ("𝐦", "Bold 'm'") } },
            { 'n', new List<(string, string)> { ("𝐧", "Bold 'n'") } },
            { 'o', new List<(string, string)> { ("𝐨", "Bold 'o'") } },
            { 'p', new List<(string, string)> { ("𝐩", "Bold 'p'") } },
            { 'q', new List<(string, string)> { ("𝐪", "Bold 'q'") } },
            { 'r', new List<(string, string)> { ("𝐫", "Bold 'r'") } },
            { 's', new List<(string, string)> { ("𝐬", "Bold 's'") } },
            { 't', new List<(string, string)> { ("𝐭", "Bold 't'") } },
            { 'u', new List<(string, string)> { ("𝐮", "Bold 'u'") } },
            { 'v', new List<(string, string)> { ("𝐯", "Bold 'v'") } },
            { 'w', new List<(string, string)> { ("𝐰", "Bold 'w'") } },
            { 'x', new List<(string, string)> { ("𝐱", "Bold 'x'") } },
            { 'y', new List<(string, string)> { ("𝐲", "Bold 'y'") } },
            { 'z', new List<(string, string)> { ("𝐳", "Bold 'z'") } },
            { 'A', new List<(string, string)> { ("𝐀", "Bold 'A'") } },
            { 'B', new List<(string, string)> { ("𝐁", "Bold 'B'") } },
            { 'C', new List<(string, string)> { ("𝐂", "Bold 'C'") } },
            { 'D', new List<(string, string)> { ("𝐃", "Bold 'D'") } },
            { 'E', new List<(string, string)> { ("𝐄", "Bold 'E'") } },
            { 'F', new List<(string, string)> { ("𝐅", "Bold 'F'") } },
            { 'G', new List<(string, string)> { ("𝐆", "Bold 'G'") } },
            { 'H', new List<(string, string)> { ("𝐇", "Bold 'H'") } },
            { 'I', new List<(string, string)> { ("𝐈", "Bold 'I'") } },
            { 'J', new List<(string, string)> { ("𝐉", "Bold 'J'") } },
            { 'K', new List<(string, string)> { ("𝐊", "Bold 'K'") } },
            { 'L', new List<(string, string)> { ("𝐋", "Bold 'L'") } },
            { 'M', new List<(string, string)> { ("𝐌", "Bold 'M'") } },
            { 'N', new List<(string, string)> { ("𝐍", "Bold 'N'") } },
            { 'O', new List<(string, string)> { ("𝐎", "Bold 'O'") } },
            { 'P', new List<(string, string)> { ("𝐏", "Bold 'P'") } },
            { 'Q', new List<(string, string)> { ("𝐐", "Bold 'Q'") } },
            { 'R', new List<(string, string)> { ("𝐑", "Bold 'R'") } },
            { 'S', new List<(string, string)> { ("𝐒", "Bold 'S'") } },
            { 'T', new List<(string, string)> { ("𝐓", "Bold 'T'") } },
            { 'U', new List<(string, string)> { ("𝐔", "Bold 'U'") } },
            { 'V', new List<(string, string)> { ("𝐕", "Bold 'V'") } },
            { 'W', new List<(string, string)> { ("𝐖", "Bold 'W'") } },
            { 'X', new List<(string, string)> { ("𝐗", "Bold 'X'") } },
            { 'Y', new List<(string, string)> { ("𝐘", "Bold 'Y'") } },
            { 'Z', new List<(string, string)> { ("𝐙", "Bold 'Z'") } },
        };

        // Cursive Alphabet
        private static readonly Dictionary<char, List<(string Character, string Name)>> CursiveAlphabet = new Dictionary<char, List<(string, string)>>
        {
            { 'a', new List<(string, string)> { ("𝓪", "Cursive 'a'") } },
            { 'b', new List<(string, string)> { ("𝓫", "Cursive 'b'") } },
            { 'c', new List<(string, string)> { ("𝓬", "Cursive 'c'") } },
            { 'd', new List<(string, string)> { ("𝓭", "Cursive 'd'") } },
            { 'e', new List<(string, string)> { ("𝓮", "Cursive 'e'") } },
            { 'f', new List<(string, string)> { ("𝓯", "Cursive 'f'") } },
            { 'g', new List<(string, string)> { ("𝓰", "Cursive 'g'") } },
            { 'h', new List<(string, string)> { ("𝓱", "Cursive 'h'") } },
            { 'i', new List<(string, string)> { ("𝓲", "Cursive 'i'") } },
            { 'j', new List<(string, string)> { ("𝓳", "Cursive 'j'") } },
            { 'k', new List<(string, string)> { ("𝓴", "Cursive 'k'") } },
            { 'l', new List<(string, string)> { ("𝓵", "Cursive 'l'") } },
            { 'm', new List<(string, string)> { ("𝓶", "Cursive 'm'") } },
            { 'n', new List<(string, string)> { ("𝓷", "Cursive 'n'") } },
            { 'o', new List<(string, string)> { ("𝓸", "Cursive 'o'") } },
            { 'p', new List<(string, string)> { ("𝓹", "Cursive 'p'") } },
            { 'q', new List<(string, string)> { ("𝕼", "Cursive 'q'") } },
            { 'r', new List<(string, string)> { ("𝓻", "Cursive 'r'") } },
            { 's', new List<(string, string)> { ("𝓼", "Cursive 's'") } },
            { 't', new List<(string, string)> { ("𝓽", "Cursive 't'") } },
            { 'u', new List<(string, string)> { ("𝓾", "Cursive 'u'") } },
            { 'v', new List<(string, string)> { ("𝓿", "Cursive 'v'") } },
            { 'w', new List<(string, string)> { ("𝔀", "Cursive 'w'") } },
            { 'x', new List<(string, string)> { ("𝔁", "Cursive 'x'") } },
            { 'y', new List<(string, string)> { ("𝔂", "Cursive 'y'") } },
            { 'z', new List<(string, string)> { ("𝔃", "Cursive 'z'") } },
            { 'A', new List<(string, string)> { ("𝓐", "Cursive 'A'") } },
            { 'B', new List<(string, string)> { ("𝓑", "Cursive 'B'") } },
            { 'C', new List<(string, string)> { ("𝓒", "Cursive 'C'") } },
            { 'D', new List<(string, string)> { ("𝓓", "Cursive 'D'") } },
            { 'E', new List<(string, string)> { ("𝓔", "Cursive 'E'") } },
            { 'F', new List<(string, string)> { ("𝓕", "Cursive 'F'") } },
            { 'G', new List<(string, string)> { ("𝓖", "Cursive 'G'") } },
            { 'H', new List<(string, string)> { ("𝓗", "Cursive 'H'") } },
            { 'I', new List<(string, string)> { ("𝓘", "Cursive 'I'") } },
            { 'J', new List<(string, string)> { ("𝓙", "Cursive 'J'") } },
            { 'K', new List<(string, string)> { ("𝓚", "Cursive 'K'") } },
            { 'L', new List<(string, string)> { ("𝓛", "Cursive 'L'") } },
            { 'M', new List<(string, string)> { ("𝓜", "Cursive 'M'") } },
            { 'N', new List<(string, string)> { ("𝓝", "Cursive 'N'") } },
            { 'O', new List<(string, string)> { ("𝓞", "Cursive 'O'") } },
            { 'P', new List<(string, string)> { ("𝓟", "Cursive 'P'") } },
            { 'Q', new List<(string, string)> { ("𝓠", "Cursive 'Q'") } },
            { 'R', new List<(string, string)> { ("𝓡", "Cursive 'R'") } },
            { 'S', new List<(string, string)> { ("𝓢", "Cursive 'S'") } },
            { 'T', new List<(string, string)> { ("𝓣", "Cursive 'T'") } },
            { 'U', new List<(string, string)> { ("𝓤", "Cursive 'U'") } },
            { 'V', new List<(string, string)> { ("𝓥", "Cursive 'V'") } },
            { 'W', new List<(string, string)> { ("𝓦", "Cursive 'W'") } },
            { 'X', new List<(string, string)> { ("𝓧", "Cursive 'X'") } },
            { 'Y', new List<(string, string)> { ("𝓨", "Cursive 'Y'") } },
            { 'Z', new List<(string, string)> { ("𝓩", "Cursive 'Z'") } },
        };

        private static readonly List<(string Character, string Description)> UnicodeModifiers = new List<(string, string)>
        {
            ("\u0300", "Combining Grave Accent"),
            ("\u0301", "Combining Acute Accent"),
            ("\u0302", "Combining Circumflex Accent"),
            ("\u0303", "Combining Tilde"),
            ("\u0304", "Combining Macron"),
            ("\u0306", "Combining Breve"),
            ("\u0307", "Combining Dot Above"),
            ("\u0308", "Combining Diaeresis"),
            ("\u030A", "Combining Ring Above"),
            ("\u0327", "Combining Cedilla"),
            ("\u031B", "Combining Horn"),
            ("\u0338", "Combining Long Solidus"),
            ("\u0340", "Combining Grave Tone Mark"),
            ("\u0341", "Combining Acute Tone Mark"),
            ("\u0328", "Combining Left Half Ring Below"),
            ("\u0329", "Combining Right Half Ring Below"),
            ("\u030D", "Combining Vertical Line"),
            ("\u030F", "Combining Inverted Breve"),
            ("\u0316", "Combining Down Tack"),
            ("\u0317", "Combining Up Tack"),
            ("\u0318", "Combining Left Tack"),
            ("\u0319", "Combining Right Tack"),
            ("\u030C", "Combining Caron"),
            ("\u033D", "Combining X Above"),
            ("\u032D", "Combining Grassy Dots"),
            ("\u0337", "Combining Short Solidus"),
            ("\u0343", "Combining Alphanumeric"),
            ("\u032C", "Combining Cursive"),
            ("\u032E", "Combining Breve Below"),
            ("\u0331", "Combining Horizontal Line Below"),
            ("\u0334", "Combining Tilde Overlay"),
            ("\u0305", "Combining Overline"),
            ("\u030B", "Combining Double Acute Accent"),
            ("\u0310", "Combining Reversed Grave"),
            ("\u0311", "Combining Reversed Acute"),
            ("\u0330", "Combining Tilde Below"),
            ("\u20DD", "Combining Enclosing Circle"),
            ("\u266A", "Combining Eighth Note"),
            ("\u0342", "Combining Grave Tone"),
            ("\u0329", "Combining Vertical Line Below"),
            ("\u0312", "Combining Left-Pointing Angle Above"),
            ("\u0313", "Combining Right-Pointing Angle Above"),
            ("\u0322", "Combining Left Curly Bracket Below"),
            ("\u0321", "Combining Right Curly Bracket Below"),
            ("\u0325", "Combining Downwards Arrow Below"),
            ("\u0339", "Combining X Below"),
            ("\u20E7", "Combining Enclosing Square"),
            ("\u030E", "Combining Vertical Line Above"),
            ("\u0344", "Combining High Tone Mark"),
            ("\u0362", "Combining Wide Bridge"),
            ("\u0345", "Combining Grapheme"),
            ("\u0346", "Combining Mid Tone Mark"),
            ("\u0347", "Combining Close Quote"),
            ("\u0348", "Combining Low Tone Mark"),
            ("\u033A", "Combining Backquote"),
            ("\u0323", "Combining Dot Below"),
            ("\u033B", "Combining Inverted Breve"),
            ("\u033C", "Combining V Above"),
            ("\u0358", "Combining Left Arrowhead"),
            ("\u0359", "Combining Right Arrowhead"),
            ("\u035A", "Combining Down Arrowhead"),
            ("\u035B", "Combining Up Arrowhead"),
            ("\u035C", "Combining Left-Pointing Triangle"),
            ("\u035D", "Combining Right-Pointing Triangle"),
            ("\u035E", "Combining Down-Pointing Triangle"),
            ("\u035F", "Combining Up-Pointing Triangle"),
            ("\u0360", "Combining Low Line"),
            ("\u0361", "Combining ZWJ"),
            ("\u0363", "Combining Long Solidus Overlay"),
            ("\u0364", "Combining Short Solidus Overlay"),
            ("\u1DC0", "Combining Macron Left Half"),
            ("\u1DC1", "Combining Macron Right Half"),
            ("\u1DC2", "Combining Macron Overlapping Left Half"),
            ("\u1DC3", "Combining Macron Overlapping Right Half"),
            ("\u1DF0", "Combining Left-Handed Tone"),
            ("\u1DF1", "Combining Right-Handed Tone"),
        };

        private int currentColorIndex = 0;
        private System.Windows.Forms.Timer colorTimer;
        private float hue; // Initialize hue to 0 for red

        public Unicode()
        {
            InitializeComponent();
            ConvertButton.Click += ConvertButton_Click;
            PopulateComboBox();
            AddModifier.Click += AddModifier_Click;
            // Start hue from red
            hue = 0.0f;

            // Initialize and start the Timer
            colorTimer = new System.Windows.Forms.Timer();
            colorTimer.Interval = 100; // Change color every 50 milliseconds
            colorTimer.Tick += ColorTimer_Tick;
            colorTimer.Start();
        }

        private void ColorTimer_Tick(object sender, EventArgs e)
        {
            hue += 0.005f;
            if (hue > 1.0f) hue = 0.0f;

            // Convert the hue to a Color
            InputLabel.BackColor = FromHSL(hue, 1.0f, 0.5f);
            OutputLabel.BackColor = FromHSL(hue, 1.0f, 0.5f);
        }

        private Color FromHSL(float h, float s, float l)
        {
            // Convert HSL to RGB
            float r = 0, g = 0, b = 0;

            if (s == 0) // Achromatic (grey)
            {
                r = g = b = l; // l is between 0 and 1
            }
            else
            {
                float q = l < 0.5 ? l * (1 + s) : l + s - (l * s);
                float p = 2 * l - q;
                r = HueToRGB(p, q, h + 1f / 3);
                g = HueToRGB(p, q, h);
                b = HueToRGB(p, q, h - 1f / 3);
            }

            return Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
        }

        private float HueToRGB(float p, float q, float t)
        {
            if (t < 0) t += 1;
            if (t > 1) t -= 1;
            if (t < 1f / 6) return p + (q - p) * 6 * t;
            if (t < 1f / 2) return q;
            if (t < 2f / 3) return p + (q - p) * (2f / 3 - t) * 6;
            return p;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Stop the timer when the form is closing
            colorTimer.Stop();
            colorTimer.Dispose();
            base.OnFormClosing(e);
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            ConvertTextButton_Click(sender, e, Input, Selector, Output);
        }

        private void AddModifier_Click(object sender, EventArgs e)
        {
            AddModifier_Clicked(sender, e, Input, Modifier);
        }

        public static void AddModifier_Clicked(object sender, EventArgs e, TextBox inputTextBox, ComboBox ComboBox)
        {
            string selectedDescription = ComboBox.SelectedItem?.ToString();

            var selectedModifier = UnicodeModifiers.FirstOrDefault(mod => mod.Description == selectedDescription);

            if (selectedModifier != default)
            {
                string character = selectedModifier.Character;

                inputTextBox.Text += character;
            }
            else
            {
                MessageBox.Show("Please select a valid Unicode modifier.");
            }

        }

        private void PopulateComboBox()
        {
            foreach (var modifier in UnicodeModifiers)
            {
                // Add the description to the ComboBox
                Modifier.Items.Add(modifier.Description);
            }

            if (Modifier.Items.Count > 0)
            {
                Modifier.SelectedIndex = 0;
            }

            if (Selector.Items.Count > 0)
            {
                Selector.SelectedIndex = 0;
            }
        }


        public static void ConvertTextButton_Click(object sender, EventArgs e, TextBox inputTextBox, ComboBox fontComboBox, TextBox outputTextBox)
        {
            string inputText = inputTextBox.Text;
            string selectedFont = fontComboBox.SelectedItem?.ToString();
            string convertedText = string.Empty;
            if (selectedFont == "Default" || selectedFont == null)
            {
                // Use confusable characters for the default option
                convertedText = ReplaceConfusables(inputText, out var confusables);
                MessageBox.Show("Used Confusables:\n" + string.Join("\n", confusables));
            }
            else if (selectedFont == "Cursive")
            {
                convertedText = ReplaceCursive(inputText, out var usedCursive);
            }
            else if (selectedFont == "Bold")
            {
                convertedText = ReplaceBold(inputText, out var usedBold);
            }
            else if (selectedFont == "Italic")
            {
                convertedText = ReplaceItalic(inputText, out var usedItalic);
            }
            else if (selectedFont == "Greek")
            {
                convertedText = ReplaceGreek(inputText, out var usedGreek);
            }

            outputTextBox.Text = convertedText;
        }

        private static string ReplaceConfusables(string input, out List<string> usedConfusables)
        {
            usedConfusables = new List<string>();
            string output = "";

            foreach (char c in input)
            {
                if (confusables.ContainsKey(c))
                {
                    var options = confusables[c];
                    var selected = options[random.Next(options.Count)];
                    output += selected.Character;
                    usedConfusables.Add(selected.Name);
                }
                else
                {
                    output += c;
                }
            }

            return output;
        }

        private static string ReplaceCursive(string input, out List<string> usedCursive)
        {
            usedCursive = new List<string>();
            string output = "";

            foreach (char c in input)
            {
                if (CursiveAlphabet.ContainsKey(c))
                {
                    var options = CursiveAlphabet[c];
                    var selected = options[random.Next(options.Count)];
                    output += selected.Character;
                    usedCursive.Add(selected.Name);
                }
                else
                {
                    output += c;
                }
            }

            return output;
        }

        private static string ReplaceGreek(string input, out List<string> usedGreek)
        {
            usedGreek = new List<string>();
            string output = "";

            foreach (char c in input)
            {
                if (GreekAlphabet.ContainsKey(c))
                {
                    var options = GreekAlphabet[c];
                    var selected = options[random.Next(options.Count)];
                    output += selected.Character;
                    usedGreek.Add(selected.Name);
                }
                else
                {
                    output += c;
                }
            }

            return output;
        }

        private static string ReplaceBold(string input, out List<string> usedBold)
        {
            usedBold = new List<string>();
            string output = "";

            foreach (char c in input)
            {
                if (BoldAlphabet.ContainsKey(c))
                {
                    var options = BoldAlphabet[c];
                    var selected = options[random.Next(options.Count)];
                    output += selected.Character;
                    usedBold.Add(selected.Name);
                }
                else
                {
                    output += c;
                }
            }

            return output;
        }

        private static string ReplaceItalic(string input, out List<string> usedItalic)
        {
            usedItalic = new List<string>();
            string output = "";

            foreach (char c in input)
            {
                if (ItalicAlphabet.ContainsKey(c))
                {
                    var options = ItalicAlphabet[c];
                    var selected = options[random.Next(options.Count)];
                    output += selected.Character;
                    usedItalic.Add(selected.Name);
                }
                else
                {
                    output += c;
                }
            }

            return output;
        }
    }
}
