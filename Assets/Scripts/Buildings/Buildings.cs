using System.Collections;
using UnityEngine;

public abstract class Buildings
{
    //Funcion que sirve para mejorar los stats de los Digimon asignados
    public abstract void StatUPG();
    //Funcion que sirve para subir de nivel el build
    public abstract void LevelUpBuild();
    //Funcion que se llama cuando sube de nivel y establece los upgrades de la base
    public abstract void Levels();
    //Funcion de tipo booleano que retorna verdadero o falso para verificar si se puede construir la build
    public abstract bool CanBuild();
    //Funcion que consume los recursos necesarios para construir la build
    public abstract void ConsumeResource();
    //Funcion que sirve para asignar un Digimon a la build
    public abstract string AssignDigimon(DigimonObject digimonToAssign);
    //IEnumerator que sirve para contar el tiempo a esperar hasta que los Digimon asignados terminen una
    //tarea, cuando la terminan, estos mejoran sus stats.
    public abstract IEnumerator ActivateDelay(DigimonObject trainedDigimon, int arrayIndex);
    //Funcion que sirve para asignar el sprite de la Build
    public abstract void SetSprite(Sprite sprite);
    //Funcion que retorna el sprite de la build
    public abstract Sprite GetSprite();
    //Funcion que establece la cantidad de recursos que requiere la build a construir
    public abstract void SetCardResource();
    public abstract int GetLevel();
}