using System;
using UnityEngine;

public partial class Controller : MonoBehaviour
{
    public bool UseAnimations { get; set; } = false;
    protected partial class Data
    {
        public SpriteRenderer sr;
        public Animator animator;
    }
    private void StartVisual()
    {
        if (UseAnimations && data.animator == null)
        {
            ShowMissingComponentError("Animator");
            
        }
    }

    private void GetVisualComponents()
    {
        data.sr = GetComponent<SpriteRenderer>();
        data.animator = GetComponent<Animator>();
    }

    public void setAnimation(String animationName)
    {
        if (data.animator){
            data.animator.Play(animationName);
        }
   
    }

    public void SetColor(Color color)
    {
        if (data.sr)
        {
            data.sr.color = color;
        }
        else
        {
            ShowMissingComponentError("SpriteRenderer");

        }
    }
}