export function getSession(key: string): any {
    try {
      const sessionData = sessionStorage.getItem(key);
      return sessionData ? JSON.parse(sessionData) : null;
    } catch (error) {
      return null;
    }
  }
  
  export function setSession(key: string, value:any): any {
    try {
      sessionStorage.setItem(key,JSON.stringify(value));
      const sessionData = sessionStorage.getItem(key);
      return sessionData ? JSON.parse(sessionData) : null;
    } catch (error) {
      return null;
    }
  }
  
  export function removeSession(key: string): any {
      try {
        sessionStorage.removeItem(key);
        return true
      } catch (error) {
        return false;
      }
    }