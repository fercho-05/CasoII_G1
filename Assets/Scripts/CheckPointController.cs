using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckPointController : MonoBehaviour
{
    /*
	 4 COLORES: VERDE, AMARILLO, ROJO Y AZUL.

	 4 ARCOS POR COLOR.
	 NO PUEDEN TOMAR UN COLOR SIN HABER TERMINADO LA SECUENCIA:
		-> NO PUEDEN TOMAR AMARILLO SIN HABER TERMINADO VERDE.
		-> NO PUEDEN TOMAR ROJO SIN HABER TERMINADO AMARILLO.
		-> NO PUEDEN TOMAR AZUL SIN HABER TERMINADO ROJO.*/


    [SerializeField] GameObject player; //Sirve para detectar al avión.
    [SerializeField] Vector3 PuntoInicio; //Sirve para poder marcar el punto de inicio de la partida.
    [SerializeField] List<GameObject> greenCheckPoints; //Esto es para manejar los 4 arcos verdes.
    [SerializeField] List<GameObject> yellowCheckPoints; //Esto es para manejar los 4 arcos amarillos.
    [SerializeField] List<GameObject> redCheckPoints; //Esto es para manejar los 4 arcos rojos.
    [SerializeField] List<GameObject> blueCheckPoints; //Esto es para manejar los 4 arcos azules.
    private List<GameObject> currentCheckPoints;  //Esto es para manejar los checkpoints.
    public int nextCheckPointIndex = 0; //Esto es para manejar el conteo de los checkpoints en su orden respectivo [Ver 1.1].
    [SerializeField] TextMeshProUGUI score;
    private int scoreCount = 0;


    /* Iguala los checkpoints actuales con los verdes, -
	 * ya que son los primeros en la secuencia.*/
    private void Start()
    {
        currentCheckPoints = greenCheckPoints;
        updatePoints();
    }


    /* Método que se ejecuta cuando el trigger de los arcos choca con el avión.
	 * Lo que consiste es que si el avión hace la secuencia correcta [Ver 1.1], -
	 * entonces mandara llamar al método de Win, caso contrario llamara al de ReseT.*/
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ingresaste." + other.gameObject.name);

        if (currentCheckPoints.Contains(other.gameObject))
        {
            Vector3 vectorPoint = player.transform.position;
            Destroy(other.gameObject);
            nextCheckPointIndex++;
            scoreCount++;

            if (nextCheckPointIndex >= currentCheckPoints.Count)
            {
                if (currentCheckPoints == greenCheckPoints)
                    currentCheckPoints = yellowCheckPoints;
                else if (currentCheckPoints == yellowCheckPoints)
                    currentCheckPoints = redCheckPoints;
                else if (currentCheckPoints == redCheckPoints)
                    currentCheckPoints = blueCheckPoints;

                nextCheckPointIndex = 0;
                if (currentCheckPoints == blueCheckPoints)
                    Win();
            }
            updatePoints();
        }
        else
        {
            ReseT();
        }
    }


    /* Método que consiste en que si el avión no hace la secuencia correcta [Ver 1.1], -
     * entonces manda un mensaje de que perdiste, luego coloca la lista de checkpoints -
     * a su estado original (cero), devuelve al avión a su posición original y coloca  -
     * los checkpoints actuales con los checkpoints verdes.*/
    private void ReseT()
    {
        Debug.Log("¡¡PERDISTES EL JUEGO, VUELVALO A INTENTAR!!.");
        nextCheckPointIndex = 0;
        player.transform.position = PuntoInicio;
        currentCheckPoints = greenCheckPoints;
        scoreCount = 0;
        updatePoints();
    }


    /* Método que consiste en que si el avión hace la secuencia correcta [Ver 1.1], -
	 * entonces manda un mensaje de que ganaste el juego, luego coloca la lista de checkpoints -
	 * a su estado original (cero), devuelve al avión a su posición original.*/
    private void Win()
    {
        Debug.Log("¡¡GANASTE EL JUEGO, FELICIDADES!!.");
        nextCheckPointIndex = 0;
        player.transform.position = PuntoInicio;
    }

    void updatePoints()
    {
        score.text = "Puntaje: " + scoreCount.ToString();

        if (scoreCount == 16)
        {
            score.text = "¡¡HAS GANADO!!";
        }
    }
}
