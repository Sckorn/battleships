using UnityEngine;
using System.Collections;

public class CustomError{
    private bool success;
    private string callClass;
    private string callMethod;
    private string errorDescription;

    public string description
    {
        get { return this.errorDescription; }
        set { this.errorDescription = value; }
    }

    public bool isSuccess
    {
        get { return this.success; }
        set { this.success = value; }
    }

    public string callerClass
    {
        get { return this.callClass; }
        set { this.callClass = value; }
    }

    public string callerMethod
    {
        get { return this.callMethod; }
        set { this.callMethod = value; }
    }

    public CustomError()
    { 
        
    }

    public CustomError(string _class, string _method)
    {
        this.callClass = _class;
        this.callMethod = _method;
    }

    public CustomError(bool _suc, string _class, string _method)
    {
        this.callClass = _class;
        this.callMethod = _method;
        this.success = _suc;
    }

    public override string ToString()
    {
        string ret = "";
        ret += "Error from class: " + this.callerClass.ToString() + "\n";
        ret += "Method: " + this.callerMethod.ToString() + "\n";
        ret += "Error description: " + this.errorDescription + "\n";
        ret += "Succes code: " + this.success.ToString();
        return ret;
    }
}
