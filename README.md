# DVDA - Damn Vulnerable Desktop App

DVDA is an intentionally vulnerable thick client project designed to help students learn and practice security testing techniques for desktop applications. It follows the **OWASP Top 10 Desktop/Thick Client Security Risks** for modern Windows apps.[^4][^5][^6]
<img width="476" height="640" alt="image" src="https://github.com/user-attachments/assets/5ea14d35-9948-4876-9881-01b92244f3f7" />

## Features

- Hands-on labs for thick client vulnerabilities
- Follows the latest **OWASP Top 10 Desktop App Security Risks**
- Source code and binary available for study/exploitation


## Screenshots

<img width="743" height="888" alt="image" src="https://github.com/user-attachments/assets/5f073731-4901-49a4-a00f-08163246e682" />


```markdown

```

Replace the example filenames with your actual image locations. This helps others understand your demo visually.[^2][^3][^1]

***

## OWASP Top 10 for Thick Clients (Brief Overview)

| OWASP Risk | Demoed in DVDA | Example |
| :-- | :--: | :-- |
| **DA1: Injection** | ✅ | SQL Injection |
| **DA2: Auth/Session** | ✅ | Registry Storage, Hardcoded Expr |
| **DA3: Sensitive Data** | ✅ | Plaintext passwords, Email |
| **DA4: Cryptography** | ⬜ | Example: Weak or missing crypto |
| **DA5: Authorization** | ⬜ | File/Folder Permissions |
| **DA6: Misconfiguration** | ✅ | Registry abuse |
| **DA7: Insecure Communication** | ⬜ | Unencrypted traffic |
| **DA8: Poor Code Quality** | ✅ | DLL hijacking, Hardcoded creds |
| **DA9: Vulnerable Components** | ⬜ | Old DLLs |
| **DA10: Logging/Monitoring** | ⬜ | Weak logging |

Source: [OWASP Desktop Security Top 10][^4]

***
<img width="1919" height="998" alt="image" src="https://github.com/user-attachments/assets/e6a5018a-3e35-4252-85f1-3d2fbb20c745" />

## Attack Labs

### 🔍 DA1: SQL Injection

```sql
-- Authentication Bypass
Username: admin' OR '1'='1' --
Password: [anything]

-- Database Enumeration
Search: ' UNION SELECT Username || ':' || Password FROM Users --
```

<img width="475" height="637" alt="image" src="https://github.com/user-attachments/assets/468390df-7b35-4c0f-9aee-ca9814f9f896" />


***

### 🗝️ DA2: Registry Session Storage

```powershell
reg query "HKCU\SOFTWARE\DVDA"
# Shows plain text Password: admin123, Username: admin
```

<img width="958" height="258" alt="image" src="https://github.com/user-attachments/assets/cb218449-a781-42f8-b76a-dec874bdbfdd" />


***

### 📊 DA3: Database Analysis

```bash
dir /s *.db
# Use DB Browser for SQLite. Table: Users (contains plain text passwords)
```

*Screenshot:*

***

### ⚡ DA4: Command Injection

```bash
test & calc          # Opens calculator
test && whoami       # Shows current user
test || dir          # Lists directory
```

*Screenshot:*

***

### 🧩 DA8: DLL Hijacking

Demonstrates how loading untrusted DLLs from the working directory can escalate privileges or expose sensitive data.[^6][^7][^4]

<img width="1676" height="195" alt="image" src="https://github.com/user-attachments/assets/dde775ff-74f3-4bae-88f4-1bb830956e9c" />


***

## Getting Started

1. Clone the repository.
2. Ensure `.NET 4.5` is installed.
3. Compile or use pre-built binary.
4. Set up SQL Server and FTP if needed (see setup instructions in repo).

## Contribution

Feel free to fork and enhance! Send a PR for new vulnerabilities, bug fixes, or better documentation.

## License

MIT

***

**Tip:** Always update image links if you move/rename image files in your repo. For more examples on embedding images, refer to [Markdown image documentation].[^3][^1][^2]
<span style="display:none">[^10][^11][^12][^13][^14][^15][^16][^17][^18][^19][^20][^21][^8][^9]</span>

<div style="text-align: center">⁂</div>

[^1]: https://github.com/orgs/community/discussions/22833

[^2]: https://cloudinary.com/guides/web-performance/4-ways-to-add-images-to-github-readme-1-bonus-method

[^3]: https://www.digitalocean.com/community/tutorials/markdown-markdown-images

[^4]: https://owasp.org/www-project-desktop-app-security-top-10/

[^5]: https://owasp.org/www-project-thick-client-top-10/

[^6]: https://github.com/srini0x00/dvta

[^7]: https://www.cyberark.com/resources/threat-research-blog/thick-client-penetration-testing-methodology

[^8]: Dvda.md

[^9]: https://owasp.org/www-project-top-ten/

[^10]: https://owasp.org/Top10/

[^11]: https://owasp.org/www-project-top-10-client-side-security-risks/

[^12]: https://www.breachlock.com/resources/blog/the-owasp-top-10-framework/

[^13]: https://checkmarx.com/glossary/owasp-top-10/

[^14]: https://github.com/secvulture/dvta

[^15]: https://stackoverflow.com/questions/14494747/how-to-add-images-to-readme-md-on-github

[^16]: https://www.optiv.com/insights/discover/blog/thick-client-application-security-testing

[^17]: https://github.com/DarkRelay-Security-Labs/Linux-Damn-Vulnerable-Thick-Client

[^18]: https://wesecureapp.com/blog/owasp-penetration-testing-your-ultimate-guide/

[^19]: https://www.scribd.com/document/727958860/Thick-Client-Pentesting-The-HackersMeetup-Version1-0pptx

[^20]: https://www.reddit.com/r/github/comments/15crgsq/how_do_i_add_images_into_my_readme_and_keep_them/

[^21]: https://www.indusface.com/blog/owasp-top-10-vulnerabilities-in-2021-how-to-mitigate-them/

