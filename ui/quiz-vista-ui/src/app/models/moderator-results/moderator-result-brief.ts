import { QuizResult } from "./quiz-result"

export interface ModeratorResultBrief{
    userName: string
    quizzes: Array<QuizResult>
}