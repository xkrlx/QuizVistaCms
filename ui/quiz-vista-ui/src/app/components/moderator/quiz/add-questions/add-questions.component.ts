import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Question } from 'src/app/models/question';
import { Answer } from 'src/app/models/answer';
import { QuestionHttpService } from 'src/app/services/http/question-http-service';
import { QuizHttpService } from 'src/app/services/http/quiz-http-service';
import { AnswertHttpService } from 'src/app/services/http/answer-http.service';


@Component({
  selector: 'app-add-questions',
  templateUrl: './add-questions.component.html',
  styleUrls: ['./add-questions.component.css']
})
export class AddQuestionsComponent {
  questions: Question[] = [];
  quizName: string = ''
  quizDetails: any;
  quizWithQuestions: any;
  isNew?: true;
  questionsToDelete: string[] = [];
  answersToDelete: string[]=[];
  newAnswers: Answer[]=[];
  quizData = [
    {
      pytanie: "pytanie1",
      odpowiedzi: {
        a: "odpowiedz1",
        b: "odpowiedz2",
        c: "odpowiedz3",
        d: "odpowiedz4"
      },
      poprawnaOdpowiedz: "a"
    },
    {
      pytanie: "pytanie2",
      odpowiedzi: {
        a: "odpowiedz1",
        b: "odpowiedz2",
        c: "odpowiedz3",
        d: "odpowiedz4"
      },
      poprawnaOdpowiedz: "c"
    }
  ];


  constructor(private route: ActivatedRoute,private quizHttpService: QuizHttpService, private questionHttpService: QuestionHttpService, private answerHttpService: AnswertHttpService,private router: Router) { }

  
  ngOnInit(): void {
    this.route.paramMap.subscribe(params =>{
      this.quizName = params.get('quizName') ?? '';
    })

    this.getQuizDetails(this.quizName)
    this.getQuestionsForQuiz(this.quizName)
  }

  onSubmit() {
    if (!this.isFormValid()) {
      alert('Każde pytanie musi mieć prawidłową odpowiedź.');
      return;
    }

    this.answersToDelete.forEach(answerId => {
      this.answerHttpService.deleteAnswer(answerId).subscribe(
        response => console.log('Odpowiedź usunięta:', response),
        error => console.error('Błąd podczas usuwania odpowiedzi:', error)
      );
    });

    this.questions.forEach(question => {
      if (question.id === 0 && question.text.trim() !== '') {
        question.quizId = this.quizDetails.id;
        this.createQuestion(question);
      } else if (question.id !== 0) {
        question.quizId = this.quizDetails.id;
        console.log(question);
        this.editQuestion(question);
      }
    });

    
    this.questionsToDelete.forEach(questionId => {
      this.questionHttpService.deleteQuestion(questionId).subscribe(
        response => console.log('Pytanie usunięte:', response),
        error => console.error('Błąd podczas usuwania pytania:', error)
      );
    });

    this.router.navigate(['/moderator/quizzez']);
  }


  addQuestion() {
    const newQuestion: Question = {
      id: 0,
      text: '',
      type: '1',
      additionalValue: 1,
      substractionalValue: 0,
      quizId: 0, 
      cmsTitleStyle: '',
      cmsQuestionsStyle: '',
      answers: [
        {
          id: 0,
          questionId: 0,
          answerText: '',
          isCorrect: false
        },
        {
          id: 0,
          questionId: 0,
          answerText: '',
          isCorrect: false
        }
      ]
    };

    this.questions.push(newQuestion);
  }

  createQuestion(question: Question) {
    this.questionHttpService.createQuestion(question).subscribe(
      response => {
        console.log('Pytanie dodane:', response);
      },
      error => {
        console.error('Wystąpił błąd przy dodawaniu pytania:', error);
      }
    );
  }

  editQuestion(question: Question) {
    if (question.id === 0) {
      console.log('Pytanie nie zostało jeszcze zapisane.');
      return;
    }
  
    this.questionHttpService.editQuestion(question).subscribe(
      response => console.log('Pytanie zaktualizowane:', response),
      error => console.error('Błąd podczas aktualizacji pytania:', error)
    );
  }

  deleteQuestion(index: number) {
      const question = this.questions[index];
  if (question.id !== 0) {
    this.questionsToDelete.push(question.id.toString());
    this.questions.splice(index, 1);
  } else {
    this.questions.splice(index, 1);
  }
  }
  

  
  
  getQuizDetails(quizName: string){
    this.quizHttpService.getQuizDetails(quizName).subscribe(res=>{
      this.quizDetails = res.model
    })
}

deleteQuiz(){
  if (this.quizDetails && this.quizDetails.id !== undefined) {
    const isConfirmed = confirm('Czy jesteś pewny, że chcesz usunąć ten quiz?');
    if (isConfirmed) {
      this.quizHttpService.deleteQuiz(this.quizDetails.id.toString()).subscribe(res => {
        this.router.navigate(['/moderator/quizzez']);
      },
      error => {
        console.error('Wystąpił błąd podczas usuwania quizu', error);
      });
    }
  } else {
    console.error('Quiz details are undefined');
  }
}


addAnswer(question: Question) {
  const newAnswer: Answer = {
    id: 0, 
    questionId: question.id, 
    answerText: '',
    isCorrect: false,
  };
  question.answers.push(newAnswer);
}

deleteAnswer(question: Question, answerIndex: number) {
  const answer = question.answers[answerIndex];

  if (question.id === 0 || answer.id === 0) {
    question.answers.splice(answerIndex, 1);
  } else {
    this.answersToDelete.push(answer.id.toString());
    question.answers.splice(answerIndex, 1);
  }
}




  editAnswer(question: Question, answer: Answer) {
    if (question.id !== 0 && answer.id !== 0) {
      this.answerHttpService.editAnswer(answer).subscribe(
        response => console.log('Odpowiedź zaktualizowana:', response),
        error => console.error('Błąd podczas aktualizacji odpowiedzi:', error)
      );
    }
  }



getQuestionsForQuiz(quizName: string) {
  this.quizHttpService.getQuizModQuestions(quizName).subscribe(res => {
    this.quizWithQuestions = res.model;

    if (this.quizWithQuestions && this.quizWithQuestions.questions) {
      this.questions = this.quizWithQuestions.questions.map((question: any) => {
        return {
          id: question.id,
          text: question.text,
          type: question.type,
          additionalValue: question.additionalValue,
          substractionalValue: question.substractionalValue,
          cmsTitleStyle: question.cmsTitleValue, 
          cmsQuestionsStyle: question.cmsQuestionsValue,
          answers: question.answers.map((answer: any) => {
            return {
              id: answer.id,
              questionId: question.id,
              answerText: answer.text,
              isCorrect: answer.isCorrect
            };
          })
        };
      });
    }
  });
}


onQuestionTypeChange(question: Question, index: number) {
  question.answers = [];

  if (question.type === '2') {
    question.answers.push({ id: 0, questionId: question.id, answerText: 'Prawda', isCorrect: false });
    question.answers.push({ id: 0, questionId: question.id, answerText: 'Fałsz', isCorrect: false });
  }
  else{
    question.answers.push({ id: 0, questionId: question.id, answerText: '', isCorrect: false });
    question.answers.push({ id: 0, questionId: question.id, answerText: '', isCorrect: false });
  }
}

isFormValid() {
  return this.questions.every(question =>
    question.text.trim() !== '' && 
    question.answers &&
    question.answers.length > 0 &&
    question.answers.some(answer => answer.isCorrect) && 
    question.answers.every(answer => answer.answerText.trim() !== '') 
  );
}
onAnswerChange(question: Question, changedAnswerIndex: number) {
  if (question.type === '1' || question.type === '2') {
    question.answers.forEach((answer, index) => {
      if (index !== changedAnswerIndex) {
        answer.isCorrect = false; 
      }
    });
  }
}

generateQuiz(): void {
  this.questions = this.quizData.map(q => ({
    id: 0, // Domyślny id dla nowego pytania
    quizId: 0, // Domyślny quizId, można dostosować w zależności od implementacji
    text: q.pytanie,
    type: '1', // Domyślny typ pytania (można dostosować)
    cmsTitleStyle: '', // Domyślny styl tytułu CMS, można dostosować
    cmsQuestionsStyle: '', // Domyślny styl pytań CMS, można dostosować
    answers: Object.entries(q.odpowiedzi).map(([key, value]) => ({
      id: 0, // Domyślny id dla nowej odpowiedzi
      questionId: 0, // Domyślny questionId, można dostosować
      answerText: value,
      isCorrect: key === q.poprawnaOdpowiedz
    })),
    additionalValue: 1, // Można dostosować
    substractionalValue: 0 // Można dostosować
  }));
}





}