import { AnswerRun } from "./answer-run";

export interface QuestionRun{
    id: number;
    text: string;
    type: string;
    additionalValue: number;
    substractionalValue: number | null;
    cmsTitleValue: string;
    cmsQuestionsValue: string;
    answers: AnswerRun[];

}