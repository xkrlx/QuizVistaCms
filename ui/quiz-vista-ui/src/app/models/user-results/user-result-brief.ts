import { QuizResultBrief } from "./quiz-result-brief"

export interface UserResultBrief{
    userName: string
    quizzes: Array<QuizResultBrief>
}