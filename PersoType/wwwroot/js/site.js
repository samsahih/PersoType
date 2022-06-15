function QuestionBuilder(question, questID, totalQuestions) {
    var ret = `<div class="c-test__question ` + ((questID == 0) ? `current` : ``) + `" id="question-` + questID + `">
        <span class="c-test__question-index" aria-hidden="true">
            Question ` + (questID + 1) + `/` + totalQuestions +
        `</span>

        <h3 class="c-test__question-text">
            You’re really busy at work and a colleague is telling you their life story and personal woes. You:
        </h3>

        <div class="c-test__question-description">
            All questions are required
        </div>

        <ul class="c-test__answers">`;
            for (var i = 0; i < question.answers.length; i++) {
                var ans = question.answers[i];
                ret +=
                    `<li class="c-test__answer">
                        <input type="radio" class="c-test__answer-input" name="answer[` + questID + `]" value="` + ans.score + `" id="question_` + questID + `_answer_` + i + `">

                        <label class="c-test__answer-label" for="question_` + questID + `_answer_` + i + `">
                            <span class="c-test__answer-index" aria-hidden="true">
                                ` + (i+1) + `
                            </span>

                            <span class="c-test__answer-text">` + ans.title + `</span>
                        </label>
                    </li>`;
             }
            ret += `</ul>`;
            ret += (questID == 0) ? `<div class="c-test__buttons">
                <button type="submit" class="c-button c-button--large c-button--secondary c-button--text-and-icon c-test__button-next" disabled="">
                    <span>Next question	></span>
                    <svg width="8" height="12"><use href="#svg-chevron-button-right" xlink:href="#svg-chevron-button-right"></use></svg>
                </button>
            </div>` : `<div class="c-test__buttons">
					<button type="submit" class="c-button c-button--large c-button--secondary c-button--text-and-icon c-test__button-prev">
						<span>< Previous</span>
					</button>

					<button type="submit" class="c-button c-button--large c-button--tertiary c-button--text-and-icon c-test__button-next" disabled="">
						<span>` + ((questID == totalQuestions - 1) ? `Finish` : `Next question >`) + `</span>
						<svg width="8" height="12"><use xlink:href="#svg-chevron-button-right"></use></svg>
					</button>
				</div>`;
        ret+=`</div>`;

    return ret;
}


function initQuestionsActions() {
    // Enable next button if answer is selected
    $(".c-test__answer").each(function () {
        var selectedAnswer = $(this);

        selectedAnswer.click(function () {
            $(this).closest('.c-test__question').find("button").prop('disabled', false);
        });
    });

    $(".c-test__button-next").click(function () {
        // Append values to url
        window.location = '#' + pushAnswerResults();

        // Move slider to next question if next button clicked
        var nextElement = $('.current').next(".c-test__question");
        
        if (nextElement.length != 0) {
            $('.current').fadeOut();
            $('.current').removeClass("current");
            nextElement.addClass("current");
        }
    });

    $(".c-test__button-prev").click(function () {
        // Append values to url
        window.location = '#' + pushAnswerResults();

        // Move slider to next question if next button clicked
        var nextElement = $('.current').prev(".c-test__question");

        if (nextElement.length != 0) {
            $('.current').fadeOut();
            $('.current').removeClass("current");
            nextElement.addClass("current");
        }
    });
}

function pushAnswerResults() {
    var input = $("input[type='radio'][name*='answer[']:checked");
    var answer = new Array(input.length);

    for (var i = 0; i < input.length; i++) {
        var a = input[i];
        //answer[i] = a.value;

        answer[i] = $(a).parent().index();
    }
    var res = "answers=[" + answer + "]";
    console.log(res);

    return res;
}