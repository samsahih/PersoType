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
            ret += (questID == 0) ? `<div class="c-button--tertiary">
                <button type="submit" class="c-button c-button--secondary c-test__button-next" disabled="">
                    <span>Next question	></span>
                </button>
            </div>` : `<div class="c-test__buttons">
					<button type="submit" class="c-button c-button--tertiary c-test__button-prev">
						<span>< Previous</span>
					</button>

					<button type="submit" class="c-button c-test__button-next" disabled="">
						<span>` + ((questID == totalQuestions - 1) ? `Finish` : `Next question >`) + `</span>
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
        } else {
            callResultsAPI();
            $("#finishScreen").show();
            removeHash();
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

function pushAnswerResults(toArray) {
    var input = $("input[type='radio'][name*='answer[']:checked");
    var answer = new Array(input.length);

    for (var i = 0; i < input.length; i++) {
        var a = input[i];
        //answer[i] = a.value;

        answer[i] = $(a).parent().index();
    }
    var res = "answers=[" + answer + "]";
    console.log(res);

    if (typeof (toArray) != "undefined" && toArray != null && toArray === true)
        return res.replace("answers=", "").replace("[", "").replace("]", "").split(",")
    else
        return res;
}



// Removes hash values from url even on unsupported older browsers
function removeHash() {
    var scrollV, scrollH, loc = window.location;
    if ("pushState" in history)
        history.pushState("", document.title, loc.pathname + loc.search);
    else {
        // Prevent scrolling by storing the page's current scroll offset
        scrollV = document.body.scrollTop;
        scrollH = document.body.scrollLeft;

        loc.hash = "";

        // Restore the scroll offset, should be flicker free
        document.body.scrollTop = scrollV;
        document.body.scrollLeft = scrollH;
    }
}

/**
     * @brief Wait for something to be ready before triggering a timeout
     * @param {callback} isready Function which returns true when the thing we're waiting for has happened
     * @param {callback} success Function to call when the thing is ready
     * @param {callback} error Function to call if we time out before the event becomes ready
     * @param {int} count Number of times to retry the timeout (default 300 or 6s)
     * @param {int} interval Number of milliseconds to wait between attempts (default 20ms)
     */
function waitUntil(isready, success, error, count, interval) {
    if (count === undefined) {
        count = 1000;
    }
    if (interval === undefined) {
        interval = 20;
    }
    if (isready()) {
        success();
        return;
    }
    // The call back isn't ready. We need to wait for it
    setTimeout(function () {
        if (!count) {
            // We have run out of retries
            if (error !== undefined) {
                error();
            }
        } else {
            // Try again
            waitUntil(isready, success, error, count - 1, interval);
        }
    }, interval);
}