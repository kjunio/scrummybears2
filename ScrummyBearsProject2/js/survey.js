function Initialize() {
    console.log('finished loading');
    document.getElementById('username').innerHTML = sessionStorage.getItem("username");
    //console.log(sessionStorage.getItem("surveyId") + sessionStorage.getItem("surveyName"));
    LoadSurvey();
}

function LoadSurvey() {
    var survId = sessionStorage.getItem("surveyId")
    var webMethod = "../WebServices.asmx/LoadSurvey";
    var parameters = "{\"surveyId\":\"" + encodeURI(survId) + "\"}";
    $.ajax({
        type: "POST",
        url: webMethod,
        data: parameters,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            console.log(msg.d);
            if (msg.d != null) {
                
                var survey = msg.d;
                
                //$("#YYYY").empty();
                for (var i = 0; i < survey.questions.length; i++) {
                    console.log([i] + survey.questions[i]);
                    //clone template, rename clone with new/correct id (e.g. q1, q2, q3 ...)
                    var question = Clone(i);
                    console.log('question object: '+ question);
                    var childElement = question.firstElementChild;
                    childElement.innerHTML = survey.questions[i];
                    document.getElementById('questionsContainer').appendChild(question);
                }                
            }
        },
        error: function (e) {
            alert("boo...");
        }
    });
}

function Clone(num) {
    var id = 'question' + num; 
    var clone = document.getElementById('questionTemplate').cloneNode(true);
    clone.id = id;
    clone.style.display = 'none';
    return clone

    //document.getElementById('surveyTab').appendChild(clone1);
}
function NextQuestion() {
    questionNumber = document.getElementById('questionNumber').value;
}

function SwitchVisibilty(id) {
    if (document.getElementById(id).style.display == 'none')
        document.getElementById(id).style.display = '';
    else
        document.getElementById(id).style.display = 'none'
}
