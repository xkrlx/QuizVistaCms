import { AttemptResultBrief } from "./attempt-result-brief"

export interface QuizResultBrief{
    quizName: string
    attempts: Array<AttemptResultBrief>
}