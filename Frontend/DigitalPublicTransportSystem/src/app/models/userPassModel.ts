export class UserPassModel {
    Username : string;
    Password : string;

    constructor(username?: string, password?: string){
        this.Username = username;
        this.Password = password;
    }
}