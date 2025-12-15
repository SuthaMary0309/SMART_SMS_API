// ============================================
// ANGULAR CHAT SERVICE EXAMPLE
// ============================================
// Place this in your Angular project: src/app/services/chat.service.ts

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ChatRequest {
  message: string;
}

export interface ChatResponse {
  response: string;
}

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private apiUrl = 'http://localhost:5283/api/chat'; // Update port if different

  constructor(private http: HttpClient) { }

  sendMessage(message: string): Observable<ChatResponse> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    const body: ChatRequest = { message };

    return this.http.post<ChatResponse>(`${this.apiUrl}/send`, body, { headers });
  }
}

// ============================================
// ANGULAR CHAT COMPONENT EXAMPLE
// ============================================
// Place this in your Angular project: src/app/components/chat/chat.component.ts

/*
import { Component, OnInit } from '@angular/core';
import { ChatService } from '../../services/chat.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  messages: Array<{ text: string; isUser: boolean }> = [];
  userMessage: string = '';
  isLoading: boolean = false;

  constructor(private chatService: ChatService) { }

  ngOnInit(): void {
    this.addMessage('Hello! How can I help you today?', false);
  }

  sendMessage(): void {
    if (!this.userMessage.trim() || this.isLoading) {
      return;
    }

    const message = this.userMessage.trim();
    this.addMessage(message, true);
    this.userMessage = '';
    this.isLoading = true;

    this.chatService.sendMessage(message).subscribe({
      next: (response) => {
        this.addMessage(response.response, false);
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Chat error:', error);
        this.addMessage('Sorry, I encountered an error. Please try again.', false);
        this.isLoading = false;
      }
    });
  }

  private addMessage(text: string, isUser: boolean): void {
    this.messages.push({ text, isUser });
    // Scroll to bottom
    setTimeout(() => {
      const chatContainer = document.querySelector('.chat-messages');
      if (chatContainer) {
        chatContainer.scrollTop = chatContainer.scrollHeight;
      }
    }, 100);
  }
}
*/

// ============================================
// ANGULAR CHAT COMPONENT HTML EXAMPLE
// ============================================
// Place this in: src/app/components/chat/chat.component.html

/*
<div class="chat-container">
  <div class="chat-messages">
    <div *ngFor="let msg of messages" [class.user-message]="msg.isUser" [class.bot-message]="!msg.isUser" class="message">
      {{ msg.text }}
    </div>
    <div *ngIf="isLoading" class="message bot-message">
      <span class="loading">AI is thinking...</span>
    </div>
  </div>
  
  <div class="chat-input">
    <input 
      [(ngModel)]="userMessage" 
      (keyup.enter)="sendMessage()" 
      placeholder="Type your message..."
      [disabled]="isLoading"
    />
    <button (click)="sendMessage()" [disabled]="isLoading || !userMessage.trim()">
      Send
    </button>
  </div>
</div>
*/

// ============================================
// ANGULAR CHAT COMPONENT CSS EXAMPLE
// ============================================
// Place this in: src/app/components/chat/chat.component.css

/*
.chat-container {
  display: flex;
  flex-direction: column;
  height: 500px;
  max-width: 800px;
  margin: 0 auto;
  border: 1px solid #ddd;
  border-radius: 8px;
  overflow: hidden;
}

.chat-messages {
  flex: 1;
  overflow-y: auto;
  padding: 20px;
  background-color: #f5f5f5;
}

.message {
  margin-bottom: 15px;
  padding: 10px 15px;
  border-radius: 8px;
  max-width: 70%;
  word-wrap: break-word;
}

.user-message {
  background-color: #007bff;
  color: white;
  margin-left: auto;
  text-align: right;
}

.bot-message {
  background-color: white;
  color: #333;
  margin-right: auto;
}

.loading {
  font-style: italic;
  color: #666;
}

.chat-input {
  display: flex;
  padding: 15px;
  background-color: white;
  border-top: 1px solid #ddd;
}

.chat-input input {
  flex: 1;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 4px;
  margin-right: 10px;
}

.chat-input button {
  padding: 10px 20px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.chat-input button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}
*/

