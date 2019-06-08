export class Korisnik {

    Username: string;
    Name: string;
    Surname: string;
    Email: string;
    Password: string;
    ConfirmPassword: string;
    Address: string;
    DateOfBirth: Date;
    UserType: string;
    ImgUrl: string;
    IsVerified: boolean;

    constructor(username?: string, firstName?: string, lastName?: string, email?: string, 
                password?: string, confirmPassword?: string, address?: string, dob?: Date, userType?: string, imgUrl?: string) {

        this.Username = username;
        this.Name = firstName;
        this.Surname = lastName;
        this.Email = email;
        this.Password = password;
        this.ConfirmPassword = confirmPassword;
        this.Address = address;
        this.DateOfBirth = dob;
        this.UserType = userType;
        this.ImgUrl = imgUrl;
        this.IsVerified = false;
    }
}