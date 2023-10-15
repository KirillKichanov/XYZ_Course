using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycles : MonoBehaviour 
    /* MonoBehaviour - базовый класс, который аттачится к гейм-обджектам. При каждом запуске
    создается гейм-обджект с наследником этого компонента. Т.е. данный скрипт наследуется от MonoBehaviour
    */ 
{
    private void Awake()
    {
        Debug.Log("Awake");
    }
    /*  Этот метод вызывается при загрузке гейм-обджекта при загрузке сцены или инициализации в определенный момент
     Так как вызывается раньше всех других методов, он хорош для самой ранней инициализации
     
     ПРИМЕР ОТ НЕЙРОСЕТКИ: Awake() - инициализация переменных и компонентов, которые должны быть доступны до Start(). Например, можно использовать 
     Awake() для получения ссылок на другие объекты, которые нужны для работы скрипта:
     
     private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
     
     */

    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }
    /* Если объект активен после Awake, вызывается OnEnable
       
       ПРИМЕР ОТ НЕЙРОСЕТКИ: OnEnable() - инициализация переменных и компонентов, которые должны быть доступны после Start(). 
       Например, можно использовать OnEnable() для подписки на события других объектов:
       
               private void OnEnable()
        {
            EventManager.OnPlayerDeath += HandlePlayerDeath;
        }

        private void HandlePlayerDeath()
        {
            // выполнить действия при смерти игрока
        }
     */

    private void Start()
    {
        Debug.Log("Start");
    }
    /* Вызывается после OnEnable. Подойдет для инициализации самого компонента
       
       ПРИМЕР ОТ НЕЙРОСЕТКИ:  Start() - инициализация переменных и компонентов, которые должны быть доступны после Awake() и OnEnable(). 
       Например, можно использовать Start() для задания начальной позиции персонажа:
       
               private void Start()
        {
            transform.position = new Vector3(0, 0, 0);
        }
     */

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate");
    }
    /* Вызывается перед обработкой физической модели юнити и не зависит от фреймрейта
       
       ПРИМЕР ОТ НЕЙРОСЕТКИ: . FixedUpdate() - обновление физики персонажа. Например, можно использовать 
       FixedUpdate() для перемещения персонажа в зависимости от нажатых клавиш:
       
               private void FixedUpdate()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontal, 0, vertical);

            rb.AddForce(movement * speed);
        }
     */

    private void Update()
    {
        Debug.Log("Update");
    }
    /* Привязан к фрейм-рейту, при снижении фпс количество вызовов метода. Обычно в этом методе
     пишется геймплейная логика
     
     ПРИМЕР ОТ НЕЙРОСЕТКИ: Update() - обновление логики игры. Например, можно использовать Update() для проверки нажатия клавиши прыжка:
     
             private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        private void Jump()
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
     */

    private void LateUpdate()
    {
        Debug.Log("LateUpdate");
    }
    /* Вызывается после обработки всех апдейтов. Бывает нужно для обработки таких скриптов, как следование камеры
     за персонажем, т.к. за время работы предыдущих апдейтов положение персонажа может измениться
     
     ПРИМЕР ОТ НЕЙРОСЕТКИ: LateUpdate() - обновление логики игры после всех остальных обновлений. 
     Например, можно использовать LateUpdate() для задания камеры, которая следит за персонажем:
     
             private void LateUpdate()
        {
            Vector3 cameraPosition = transform.position - transform.forward * cameraDistance + Vector3.up * cameraHeight;
            Camera.main.transform.position = cameraPosition;
            Camera.main.transform.LookAt(transform.position);
        }
     */

    private void OnDisable()
    {
        Debug.Log("OnDisable");
    }
    /* Вызывается в момент выключения или уничтожения гейм-обджекта
     
       ПРИМЕР ОТ НЕЙРОСЕТКИ: OnDisable() - очистка ресурсов, которые были созданы в OnEnable(). 
       Например, можно использовать OnDisable() для отписки от событий других объектов:
       
               private void OnDisable()
        {
            EventManager.OnPlayerDeath -= HandlePlayerDeath;
        }
     */

    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }
    /* Вызывается в момент уничтожения гейм-обджекта, уничтожения сцены или закрытии проекта
     
       ПРИМЕР ОТ НЕЙРОСЕТКИ: OnDestroy() - очистка ресурсов, которые были созданы в Awake() или Start(). 
       Например, можно использовать OnDestroy() для удаления объектов, которые были созданы в игре:
       
               private void OnDestroy()
        {
            Destroy(gameObject);
        }
     */
}
