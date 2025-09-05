# ShopVault - Instructor's Answer Key

**🔑 Solutions and Expected Results for Lab Exercises**

---

## ⚠️ INSTRUCTOR USE ONLY

This document contains the solutions and expected results for all ShopVault lab exercises. Use this to guide students and verify their findings.

---

## Lab 1 Solutions: SQL Injection

### Exercise 1.1: Authentication Bypass

**Working Payloads**:
```sql
admin' OR '1'='1' --        ✅ Most reliable
admin' OR 1=1 --           ✅ Alternative syntax  
' OR ''=''--               ✅ Always true condition
admin'/**/OR/**/1=1--      ✅ Comment-based bypass
```

**Expected Results**:
- Student should successfully bypass authentication
- Application should log them in as the first user in the database (typically 'admin')
- Login screen should be bypassed completely

**Common Student Issues**:
- Forgetting the `--` comment syntax
- Not understanding case sensitivity in SQL
- Missing single quotes around conditions

### Exercise 1.2: Data Extraction

**Working UNION Payloads**:
```sql
' UNION SELECT Username, Password, Email, 'x', 'x', 'x', 'x', 'x' FROM Users --
```

**Expected Database Schema**:
```sql
Users Table:
- Id (INTEGER)
- Username (TEXT)
- Password (TEXT) 
- Email (TEXT)
- IsAdmin (INTEGER)
- CreatedAt (DATETIME)

Products Table:
- Id (INTEGER)
- Name (TEXT)
- Description (TEXT)
- Price (REAL)
- Category (TEXT)
- ImageUrl (TEXT)
- Stock (INTEGER)
- CreatedAt (DATETIME)
```

**Expected Data Extraction**:
```
Users:
admin:admin123:admin@shopvault.com
john_doe:password:john@email.com  
jane_smith:123456:jane@email.com
test_user:test:test@email.com
vulnerable:qwerty:vuln@email.com
```

---

## Lab 2 Solutions: Authentication Security

### Exercise 2.1: Password Policy Analysis

**Accepted Weak Passwords**:
- ✅ `admin123` (default admin password)
- ✅ `password` (common password)
- ✅ `123456` (sequential numbers)
- ✅ `test` (simple dictionary word)
- ✅ `qwerty` (keyboard pattern)

**Password Policy Findings**:
- No minimum length requirement
- No complexity requirements
- No character diversity requirements
- No dictionary word checking
- Default credentials not forced to change

**Account Lockout Testing**:
- No account lockout after multiple failures
- Unlimited login attempts allowed
- No delay between failed attempts
- No CAPTCHA or rate limiting

### Exercise 2.2: Session Management

**Expected Findings**:
- Sessions may persist after application restart
- No proper session timeout implemented
- Session tokens (if any) are predictable or reused
- Multiple concurrent sessions allowed without restriction

---

## Lab 3 Solutions: Data Security

### Exercise 3.1: Database Analysis

**Database Location**: `%TEMP%\ShopVault\vulnmart.db`

**Critical Findings**:
```sql
-- Password storage analysis
SELECT Username, Password FROM Users;
-- Result: Passwords stored in PLAIN TEXT (major vulnerability)

-- Database structure
.schema
-- Shows all table structures with no encryption
```

**Security Issues Identified**:
- Passwords stored in plain text (no hashing)
- No encryption at rest
- Database file world-readable
- Sensitive PII stored without protection

### Exercise 3.2: Memory Analysis

**Expected Memory Findings**:
- Usernames and passwords visible in memory dumps
- Database connection strings in memory
- SQL queries containing sensitive data
- Session tokens or authentication data

**Tools Output Examples**:
```
Process Hacker String Search Results:
- "admin123" (password in memory)
- "SELECT * FROM Users WHERE Username='admin'" (SQL queries)
- Connection strings with database paths
```

---

## Lab 4 Solutions: Access Control

### Exercise 4.1: Privilege Escalation

**Admin Functions to Test**:
- User management interface
- Product management system
- Security testing panel
- System configuration options

**Expected Vulnerabilities**:
- Direct URL access to admin functions
- Missing server-side authorization checks
- Client-side only access controls
- Role parameter manipulation possible

**Successful Escalation Indicators**:
- Regular user can access admin features
- Admin-only data becomes visible
- Administrative functions can be executed

### Exercise 4.2: Horizontal Privilege Escalation

**Test Scenarios**:
- View other users' profile information
- Access other users' shopping cart data
- View other users' order history
- Modify other users' account details

**Expected Results**:
- User ID parameter manipulation works
- Direct object references not protected
- Cross-user data access possible

---

## Lab 5 Solutions: Input Validation

### Exercise 5.1: XSS Testing

**Locations Where XSS Works**:
```html
Product Name Fields:
<script>alert('XSS in Product')</script>

Search Functionality:  
<img src=x onerror=alert('Search XSS')>

User Profile Fields:
<svg onload=alert('Profile XSS')>
```

**Expected XSS Execution**:
- Alert dialogs should appear
- JavaScript code executes in application context
- Stored XSS persists across sessions
- Reflected XSS executes immediately

### Exercise 5.2: Command Injection

**Working Payloads**:
```bash
test & whoami
# Expected: Shows current Windows username

test && dir  
# Expected: Lists current directory contents

test || echo "Command injection successful"
# Expected: Displays the echo message

test; netstat -an
# Expected: Shows network connections
```

**Evidence of Success**:
- System command output displayed in application
- Operating system information disclosed
- File system access demonstrated

---

## Lab 6 Solutions: File System Security

### Exercise 6.1: File Permissions

**Expected Permission Issues**:
```cmd
icacls ShopVault-VulnerableApp.exe
# Should show overly permissive permissions

icacls vulnmart.db  
# Database file readable/writable by all users
```

**Security Issues**:
- Application files modifiable by standard users
- Database files world-accessible
- Configuration files contain sensitive data
- Temporary files not properly secured

---

## Lab 7 Solutions: Network Analysis

### Exercise 7.1: Traffic Analysis

**Expected Network Findings**:
- Login credentials transmitted in clear text (if network component exists)
- Database queries visible in network traffic
- Session tokens transmitted unencrypted
- Sensitive data in HTTP headers or parameters

**Wireshark Filter Examples**:
```
# Filter for HTTP traffic
http

# Search for credentials
http contains "password"

# Look for sensitive data
http contains "admin"
```

---

## Lab 8 Solutions: Configuration Security

### Exercise 8.1: Misconfigurations

**Default Configuration Issues**:
- Default admin credentials enabled
- Debug information in error messages
- Detailed stack traces displayed
- Unnecessary administrative features enabled

**Error Message Analysis**:
- SQL error messages reveal database structure
- File path information disclosed in errors
- System information leaked in exceptions
- Database connection details in error output

---

## Lab 9 Solutions: Logging and Monitoring

### Exercise 9.1: Security Logging

**Expected Logging Deficiencies**:
- Failed login attempts not logged
- SQL injection attempts not detected
- No security event correlation
- Sensitive data logged in plain text

**Missing Log Events**:
- Authentication failures
- Authorization violations  
- Input validation failures
- Privilege escalation attempts
- Data access patterns

---

## Assessment Rubric

### Scoring Guide (100 points total)

| Category | Excellent (90-100) | Good (75-89) | Satisfactory (60-74) | Needs Improvement (<60) |
|----------|-------------------|--------------|---------------------|------------------------|
| SQL Injection (20 pts) | Identifies and exploits all injection points, extracts complete database | Identifies most injection points, partial data extraction | Basic injection successful, limited data extraction | Cannot perform successful injection |
| Authentication (15 pts) | Comprehensive analysis of all auth weaknesses | Identifies most auth issues | Basic password policy analysis | Limited understanding of auth flaws |
| Access Control (15 pts) | Demonstrates both vertical and horizontal escalation | Shows one type of privilege escalation | Identifies access control issues | Cannot demonstrate escalation |
| Data Security (15 pts) | Complete analysis of data storage and memory issues | Identifies most data security problems | Basic data security assessment | Limited data security understanding |
| Input Validation (15 pts) | Successful XSS and command injection | Demonstrates one type of injection | Identifies input validation issues | Cannot exploit input validation |
| Report Quality (20 pts) | Professional report with clear remediation | Good documentation with most details | Basic reporting with some gaps | Poor documentation |

### Common Student Mistakes

1. **SQL Injection**:
   - Forgetting comment syntax (`--`)
   - Incorrect column count in UNION queries
   - Case sensitivity issues

2. **Authentication Testing**:
   - Not testing for account lockout
   - Missing session analysis
   - Incomplete password policy assessment

3. **Access Control**:
   - Only testing vertical escalation
   - Missing horizontal privilege testing
   - Not documenting impact properly

4. **Documentation**:
   - Missing screenshots as evidence
   - Incomplete remediation recommendations
   - Poor risk assessment

### Remediation Teaching Points

**For Each Vulnerability, Emphasize**:

1. **Root Cause**: Why the vulnerability exists
2. **Technical Fix**: Specific code changes needed  
3. **Process Improvement**: How to prevent in development
4. **Detection**: How to identify in testing
5. **Business Impact**: Why it matters to organization

---

## Extended Learning Activities

### Advanced Exercises

1. **Custom Exploit Development**:
   - Students write Python scripts to automate SQL injection
   - Create proof-of-concept exploits for identified vulnerabilities
   - Develop detection signatures for security tools

2. **Threat Modeling**:
   - Create threat models for ShopVault
   - Identify attack vectors and mitigations
   - Design security controls for remediation

3. **Security Architecture Review**:
   - Propose secure architecture alternatives
   - Design secure authentication systems
   - Create security control matrices

### Real-World Applications

1. **Code Review Exercises**:
   - Analyze vulnerable code snippets
   - Identify security anti-patterns
   - Propose secure coding alternatives

2. **Compliance Mapping**:
   - Map vulnerabilities to compliance frameworks (PCI DSS, GDPR, etc.)
   - Create compliance gap analysis
   - Develop remediation roadmap

---

## FAQ for Instructors

**Q: What if students can't find the database file?**
A: Guide them to check `%TEMP%\ShopVault\` directory. The application creates it there on first run.

**Q: SQL injection isn't working for some students?**  
A: Check they're using proper comment syntax (`--`) and correct quote handling.

**Q: How do I reset the application state?**
A: Delete the database file in temp directory - application will recreate it.

**Q: Students report application won't start?**
A: Ensure .NET Framework 4.7.2 is installed and check Windows compatibility.

**Q: How long should the full lab take?**
A: Expect 4-6 hours for complete walkthrough, 2-3 hours for experienced students.

---

*This answer key is for instructor use only. Maintain confidentiality to preserve learning value for students.*
