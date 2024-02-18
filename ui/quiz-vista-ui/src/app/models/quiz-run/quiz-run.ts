import { QuestionRun } from "./question-run";

export interface QuizRun{
    model: {
        name: string;
        userAttemptCount: number;
        authorName: string;
        questions: QuestionRun[];
      };
      isValid: boolean;
      errorMessage: string;
    
}