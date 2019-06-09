export class ChangePassModel{
    OldPassword : string;
    NewPassword : string;
    ConfirmPassword : string;

    constructor(oldPassword?: string, newPassword?: string, confirmPassword?: string){
        this.OldPassword = oldPassword;
        this.NewPassword = newPassword;
        this.ConfirmPassword = confirmPassword;
    }
}

