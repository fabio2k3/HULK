using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime;
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Data;
using System.Collections.Concurrent;
using System.Linq.Expressions;


bool check = true;
while(check)
{
    Console.Write("> ");
    string expression = Console.ReadLine();
    
    if (expression.StartsWith("print"))
    {
        Parser parser = new Parser();
        string evaluate = expression.Replace(" ", "");
        if ((evaluate.Contains("+")) || (evaluate.Contains("-")) || (evaluate.Contains("*")) || (evaluate.Contains("/")) || (evaluate.Contains("^")))
        {
            double result = parser.Parse(evaluate.Substring(6, evaluate.Length - 8));
            Console.WriteLine(result);
        }
        else
            Console.WriteLine(expression.Substring(6, evaluate.Length - 8));
        
    }
    else if(expression.StartsWith("let"))
    {
        LetIn letin = new LetIn(expression);
        string[] arrayOfExpression = expression.Split(',');
        letin.CheckEvaluate(arrayOfExpression);
    }
    
    if (expression ==  string.Empty) 
    {
        Console.WriteLine("Ha terminado su EXPEREINCIA con H.U.L.K");
        check = false;
    }
}

Console.ReadKey();

#region Let-In
public class LetIn
{
    Dictionary<int,string> letToin = new Dictionary<int,string>();
    Dictionary<int,string> afterIn = new Dictionary<int,string>();
    string input;
    string[] arrayOfinput;
    int count;

    public LetIn(string input)
    {
        this.input = input.Replace(" ","");
        arrayOfinput = input.Split(' ');
        count = 0;
        this.letToin = new Dictionary<int,string>();
        this.afterIn = new Dictionary<int,string>();
    }

    public Dictionary<int,string> CompleteLetToIn()
    {
        foreach(string s in arrayOfinput)
        {
            if (s == "let")
                continue;
            else if (s == "in")
                break;
            else
            {
                letToin.Add(count, s);
                count++;
            }
                
        }
        return letToin;
    }

    public Dictionary<int,string> CompleteAfterIn()
    {
        int count = 1;
        for (int i = 0; i < arrayOfinput.Length; i++)
        {
            if (arrayOfinput[i] == "in")
            {
                for(int j = 0; j < arrayOfinput.Length; j++)
                {
                    if (i + count >= arrayOfinput.Length)
                        break;

                    afterIn.Add(j, arrayOfinput[i + count]);
                    count++;
                }
            }
        }
        return afterIn;
    }

    public void Evaluate(string[] array)
    {
        CompleteLetToIn();
        CompleteAfterIn();
        for (int i = 0; i < letToin.Count; i++)
        {
            if (letToin[i] == "=")
            {
                for (int j = 0;  j < afterIn.Count; j++)
                {
                    if (afterIn[j] == letToin[i-1])
                    {
                        afterIn[j] = letToin[i + 1];
                    }
                }
            }
        }
    }

    public string ConvertDictionaryToString(Dictionary<int,string> dictionary)
    {
        string result = "";
        foreach(KeyValuePair<int,string> kvp in dictionary)
        {
            result += kvp.Value;
        }

        return result;
    }

    public void CheckEvaluate(string[] array)
    {
        Evaluate(array);
        if (afterIn[0] == "print")
        {
            string check = ConvertDictionaryToString(afterIn);
            if ((check.Contains("+")) || (check.Contains("-")) || (check.Contains("*")) || (check.Contains("/")) || (check.Contains("^")))
            {
                Parser parser = new Parser();
                double result = parser.Parse(check.Substring(6, check.Length - 8));
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine(check.Substring(6, check.Length - 7));
            }
        }
    }
}
#endregion

#region Clase Calcular Expresiones ALgebraicas
public class Parser
{
    private string input;
    private int position;

    public double Parse(string expresion)
    {
        input = expresion.Trim();
        position = 0;
        return ParseExpression();
    }

    private double ParseExpression()
    {
        double leftValue = ParseTerm();

        while (position < input.Length)
        {
            char op = input[position];
            if (op != '+' && op != '-')
                break;
        
            position++;

            double rightValue = ParseTerm();
            if (op == '+')
                leftValue += rightValue;
            else if (op == '-')
                leftValue -= rightValue;
        }
        return leftValue;
    }
    private double ParseTerm()
    {
        double leftValue = ParseExp();

        while (position < input.Length) 
        { 
            char op = input[position];
            if(op != '*' && op != '/')
                break;

            position++;

            double rightValue = ParseExp();

            if(op == '*')
                leftValue *= rightValue; 
            else if (op == '/')
                leftValue /= rightValue;
        }

        return leftValue;
    }

    private double ParseExp()
    {
        double leftValue = ParseFactor();
        while (position < input.Length)
        {
            char op = input[position];
            if (op != '^')
                break;

            position++;

            double rightValue = ParseExp();

            if (op == '^')
                leftValue = Math.Pow(leftValue,rightValue);
        }
        return leftValue;
    }
    private double ParseFactor()
    {
        if (position >= input.Length)
            throw new ArgumentException("Invalid expression");

        char currentChar = input[position];

        if(char.IsDigit(currentChar))
        {
            string number = "";
            while(position < input.Length && char.IsDigit(input[position])) 
            { 
                number += input[position];
                position++;
            }

            return double.Parse(number);
        }
        else if( currentChar == '(')
        {
            position++;
            double value = ParseExpression();
            if ((position >= input.Length) || (input[position] != ')'))
                throw new ArgumentException("Invalid expression");
            position++;
            return value;
        }
        else
        {
            throw new ArgumentException("Invalid Expression");
        }
    }
}
#endregion




