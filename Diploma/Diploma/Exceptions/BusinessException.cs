﻿using System;

namespace Diploma.Exceptions
{
    public class BusinessException: Exception
    {
        public BusinessException()
        {
            
        }

        public BusinessException(string message)
            :base(message)
        {
        }
    }
}
