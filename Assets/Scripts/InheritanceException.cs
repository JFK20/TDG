using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InheritanceException : Exception {
    public InheritanceException() 
        :base("Shouldn't be called get from inheritance") {}
    
    public InheritanceException(string msg)
        :base(msg) { }
    
    public InheritanceException(string msg, Exception inner)
        :base(msg,inner) { }
    }
