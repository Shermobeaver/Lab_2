using System;
using System.ComponentModel;

namespace ClassLibrary
{
    public class InputParams : IDataErrorInfo
    {
        // Properties
        private int _length;
        public int Length
        {
            get => _length;
            set
            {
                _length = value;
                Error1 = false;
            }
        }

        private int _uniform_length;
        public int UniformLength
        {
            get => _uniform_length;
            set
            {
                _uniform_length = value;
                Error2 = false;
            }
        }

        private double _left;
        public double Left
        {
            get => _left;
            set
            {
                _left = value;
                Error1 = false;
            }
        }
        private double _right;
        public double Right
        {
            get => _right;
            set
            {
                _right = value;
                Error1 = false;
            }
        }
        private double _x1;
        public double x1
        {
            get => _x1;
            set
            {
                _x1 = value;
                Error2 = false;
            }
        }
        private double _x2;
        public double x2
        {
            get => _x2;
            set
            {
                _x2 = value;
                Error2 = false;
            }
        }
        private double _x3;
        public double x3
        {
            get => _x3;
            set
            {
                _x3 = value;
                Error2 = false;
            }
        }

        public SPf Function { get; set; }
        public bool Error1 { get; set; }
        public bool Error2 { get; set; }

        // Constructor
        public InputParams(int inLength1, int inLength2, double inLeft, double inRight, SPf inFunction, double inx1, double inx2, double inx3)
        {
            Length = inLength1;
            UniformLength = Length * 10;
            Left = inLeft;
            Right = inRight;
            Function = inFunction;
            x1 = inx1;
            x2 = inx2;
            x3 = inx3;
        }

        // IDataErrorInfo
        public string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Length":
                        if ((Length < 3) || (Length > 100000))
                        {
                            error = "Length";
                            Error1 = true;
                        }
                        break;
                    case "Right":
                    case "Left":
                        if (Right < Left)
                        {
                            error = "Borders";
                            Error1 = true;
                        }
                        break;
                    case "UniformLength":
                        if ((UniformLength < 3) || (UniformLength > 100000))
                        {
                            error = "UniformLength";
                            Error2 = true;
                        }
                        break;
                    case "x1":
                    case "x2":
                    case "x3":
                        if ((Left > x1) || (x1 > x2) || (x2 > x3) || (x3 > Right))
                        {
                            error = "Limits";
                            Error2 = true;
                        }
                        break;
                    default:
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
