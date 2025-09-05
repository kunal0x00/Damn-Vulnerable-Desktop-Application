# DVDA - Damn Vulnerable Desktop Application

## 🔓 Educational Security Testing Platform

DVDA is an intentionally vulnerable desktop application built with C# WinForms and SQLite, designed for educational purposes and security testing. It demonstrates the **OWASP Top 10 Desktop Application Security Risks**.

## ⚠️ WARNING
This application contains **intentional security vulnerabilities** and should only be used in controlled environments for educational purposes, penetration testing training, and security research.

## 🎯 Features

### Core Application
- **Modern Material Design UI** using MaterialSkin
- **E-commerce Simulation** with product catalog, cart, and checkout
- **Admin Dashboard** with user management
- **Multi-user Authentication** system
- **SQLite Database** backend
- **Comprehensive Logging** system

### Security Laboratory
Interactive testing environment for all OWASP Top 10 Desktop vulnerabilities:

| Vulnerability | Description | Test Features |
|---------------|-------------|---------------|
| **DA1 - Injection** | SQL and Command Injection | Interactive SQL injection testing, Command execution |
| **DA2 - Broken Authentication** | Weak session management | Predictable session tokens, No timeout |
| **DA3 - Sensitive Data Exposure** | Hardcoded secrets, logging | Exposed passwords, API keys, sensitive logs |
| **DA4 - Improper Cryptography** | Weak hashing algorithms | MD5 usage, weak encryption |
| **DA5 - Improper Authorization** | Missing access controls | Admin bypass, role escalation |
| **DA6 - Security Misconfiguration** | Poor security settings | File upload vulnerabilities, stack traces |
| **DA7 - Insecure Communication** | Unencrypted data transmission | Plaintext data storage/transfer |
| **DA8 - Poor Code Quality** | Buffer overflows, race conditions | Memory safety issues |
| **DA9 - Vulnerable Components** | Outdated dependencies | Component security analysis |
| **DA10 - Insufficient Logging** | Poor monitoring | Sensitive data in logs |

## 🚀 Getting Started

### Prerequisites
- Windows 10/11
- .NET Framework 4.7.2+
- Visual Studio 2019+ (recommended)

### Installation
1. Clone the repository
2. Open `DVDAv2.sln` in Visual Studio
3. Restore NuGet packages
4. Build and run the application

### Default Credentials
- **Admin**: `admin` / `admin123`
- **Test User**: `testuser` / `password123`

## 🔧 Architecture

```
DVDAv2/
├── Forms/
│   ├── LoginForm.cs              # Authentication with SQL injection
│   ├── MainForm.cs               # Dashboard with Security Lab
│   ├── ProductsForm.cs           # E-commerce catalog
│   ├── SecurityLabForm.cs        # Vulnerability testing
│   └── AdminDashboardForm.cs     # User management
├── Core/
│   ├── VulnerabilityManager.cs   # All OWASP Top 10 implementations
│   ├── ThemeManager.cs           # UI styling and theming
│   └── DatabaseHelper.cs         # Vulnerable data access
└── Database/
    └── vulnmart.db               # SQLite database
```

## 🎓 Educational Use Cases

### For Students
- Learn about desktop application security
- Practice identifying vulnerabilities
- Understand secure coding principles
- Study OWASP Top 10 Desktop risks

### For Security Professionals
- Penetration testing training
- Security assessment practice
- Vulnerability research
- Tool testing and validation

### For Developers
- Secure coding education
- Code review training
- Security testing methodologies
- Defensive programming techniques

## 🛡️ Security Testing Examples

### SQL Injection
```
Username: admin' OR '1'='1' --
Password: anything
```

### Command Injection
```
Input: test & calc
Result: Opens calculator via command injection
```

### Authorization Bypass
```
Username: normaluser_admin
Result: Gets admin access due to string matching
```

## 📝 Vulnerability Catalog

Each vulnerability includes:
- **Real-world example** implementation
- **Interactive testing** interface  
- **Educational explanations** of the risk
- **Remediation guidance** and best practices
- **OWASP mapping** to standard classifications

## 🔍 Security Testing Tools Integration

The application is designed to work with:
- **Static Analysis Tools** (SonarQube, Veracode)
- **Dynamic Analysis Tools** (OWASP ZAP, Burp Suite)
- **Dependency Scanners** (OWASP Dependency Check)
- **Code Quality Tools** (CodeQL, Semgrep)

## 📚 Learning Resources

- [OWASP Desktop App Security Top 10](https://owasp.org/www-project-desktop-app-security-top-10/)
- [Secure Coding Practices](https://owasp.org/www-project-secure-coding-practices-quick-reference-guide/)
- [Application Security Verification Standard](https://owasp.org/www-project-application-security-verification-standard/)

## ⚖️ Legal Disclaimer

This software is provided for educational and research purposes only. Users are responsible for ensuring they have proper authorization before testing on any systems. The authors are not responsible for any misuse of this software.

## 🤝 Contributing

Contributions are welcome! Please:
1. Focus on educational value
2. Document security implications
3. Follow responsible disclosure
4. Maintain code clarity for learning

## 📞 Support

For educational use and security research inquiries, please open an issue on the repository.

---

**Remember**: This is a vulnerable application by design. Never deploy this in production environments!
