export class ResultWithModel<T>{
    IsValid: boolean = false;
    Model: T;

    constructor(_model: T, _isValid: boolean) {
        this.Model = _model;
        this.IsValid = _isValid;
        
    }
}