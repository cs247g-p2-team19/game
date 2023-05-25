using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Word : AutoMonoBehaviour
{
    public Bounds Bounds => collider.bounds;

    public string word;
    
    [Required]
    public Collider2D collider;
}