﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Data
{
    public class BookRent : BookEvent
    {
        public BookRent(Reader reader, BookState bookState, DateTime dateTime) : base(reader, bookState, dateTime)
        {
        }

        public BookRent(Reader reader, BookState bookState) : base(reader, bookState)
        {
        }

        public override bool Equals(object obj)
        {
            return obj is BookRent && base.Equals(obj);
        }

        public override int GetHashCode()
        {
            int hashCode = 13120312;
            return hashCode * -1521134295 + base.GetHashCode();
        }

        public override string ToString()
        {
            return "Rent - " + base.ToString();
        }
    }
}
