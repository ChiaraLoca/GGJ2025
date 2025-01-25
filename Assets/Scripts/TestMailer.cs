using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TestMailer : MonoBehaviour
{
   bool _repeate = true;
    // Start is called before the first frame update
    void Start()
    {
        //MailController.SendEmail("ronchi117@gmail.com","SCOMUNICA!",MessageHelper.GetMailTextScomunica("MATTIA"));
    }

    // Update is called once per frame
    void Update()
    {
       // StartCoroutine(Wait8SecondsAndSendMail());
    }

    private IEnumerator Wait8SecondsAndSendMail()
    {

        yield return new WaitForSeconds(8);
        if(_repeate)
        {
            MailController.RetrieveEmail();
            _repeate = false;   
        }
           
     
    }
}
