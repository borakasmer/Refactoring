using System;

public class Register : ICloneable
{ 
    public string courseID { get; set; } 
    public int student { get; set; }

    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public Course course { get; set; }    
    public int amount { get; set; }    
    public decimal valumeCredit { get; set; }
}