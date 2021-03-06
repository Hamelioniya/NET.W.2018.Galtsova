﻿using System;
using BLL.Interface.Interfaces;

namespace BLL.Generators
{
    public class IDGenerator : IIDGenerator
    {
        private int _id;

        public IDGenerator() : this(1)
        {
        }

        public IDGenerator(int id)
        {
            Id = id;
        }

        private int Id
        {
            get
            {
                return _id;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("The id must be greater than 0.", nameof(Id));
                }

                _id = value;
            }
        }

        public int GetId()
        {
            int result = Id;
            Id++;

            return result;
        }
    }
}
