
using UnityEngine;
using System;

public class PrintCommand : Command
{

    private String Text;
    
    public PrintCommand(  String text, Controller controller = null, int priority=5){
        this.Controller = controller;
        this.Priority = priority;
        this.Text = text;
    }
    public override void Execute()
    {
        Controller.Print(Text);
    }
}