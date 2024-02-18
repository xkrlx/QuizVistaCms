import { AttemptResultBrief } from "../user-results/attempt-result-brief"


export interface QuizResult{
    userName: string
    attempts: Array<AttemptResultBrief>
}