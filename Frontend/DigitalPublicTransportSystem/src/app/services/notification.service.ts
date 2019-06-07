import { EventEmitter, Injectable } from '@angular/core';
@Injectable({ providedIn: 'root'})
export class NotificationService {
    notifyEvent: EventEmitter<string> = new EventEmitter<string>();
    sessionEvent: EventEmitter<boolean> = new EventEmitter<boolean>();
    userEditedEvent: EventEmitter<boolean> = new EventEmitter<boolean>();

    constructor() { }

}