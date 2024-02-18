import { Answer } from "./answer";

export interface Question {
    id: number;
    type: string;
    text: string;
    additionalValue: number;
    substractionalValue: number;
    quizId: number;
    cmsTitleStyle: string;
    cmsQuestionsStyle: string;
    answers: Answer[];
  }