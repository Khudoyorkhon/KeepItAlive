using UnityEngine;
using Pathfinding;

namespace KeepItAlive
{
    public class Enemy : MonoBehaviour, IAttack, IPooledObject
    {
        #region Private Variable

        [SerializeField] private float Speed = 20f;
        [SerializeField] private float nextWayPointDistance = 3f;

        [SerializeField] private Path path;

        [SerializeField] private int currentWayPoint = 0;

        private bool _reachEndPoint = false;
        [SerializeField] private bool _flyEnemy = false;
        [SerializeField] private bool _groundEnemy = false;
                    

        [SerializeField] private Seeker seeker = null;
        [SerializeField] private Rigidbody2D _enemyRigidbody = null;

        private float _distance = 0f;
        private float _distanceToTarget = 0;
        private float _nextAttackTime;
        

        private Vector2 _direction;

        private Vector3 _scale;

        private bool attack = false;
        #endregion

        #region Public Varibale
        public Transform Target, AttackPoint;

        public float AttackDistance = 8f, AttackRange;

        public LayerMask PlayerLayer;
        public Rigidbody2D EnemyRigidbody { get => _enemyRigidbody; set => _enemyRigidbody = value; }

        public Animator EnemyAnimator;

        public float AttackRate;
        public int Health, Damage;
        #endregion

        private void Start()
        {
            
            attack = false;
            _scale = transform.localScale;
            InvokeRepeating("UpdatePath", 0f, 0.5f);

        }
        public void OnObjectSpawn()
        {

            attack = false;
            _scale = transform.localScale;
            InvokeRepeating("UpdatePath", 0f, 0.5f);
        }
 

        private void UpdatePath()
        {
            if(Target != null)
            {
                if (seeker.IsDone())
                    seeker.StartPath(_enemyRigidbody.position, Target.position, OnCompletePath);
            }
           
        }

        private void FixedUpdate()
        {
            UpdateAnimtion();
            FindTarget();
        }

        private void OnCompletePath(Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWayPoint = 0;
            }
        }
        public void FindTarget()
        {
            if (path == null)
                return;

            if (currentWayPoint >= path.vectorPath.Count)
            {
                _reachEndPoint = true;
                return;
            }
            else
            {
                _reachEndPoint = false;
            }

            _direction = ((Vector2)path.vectorPath[currentWayPoint] - _enemyRigidbody.position).normalized;

            if (_flyEnemy)
            {
                _enemyRigidbody.velocity = _direction * Speed * Time.deltaTime;
            }

            if (_groundEnemy)
            {
                _enemyRigidbody.velocity = new Vector2(_direction.x * Speed * Time.deltaTime, _enemyRigidbody.velocity.y);
            }

            CheckDistance();

            _distance = Vector2.Distance(_enemyRigidbody.position, path.vectorPath[currentWayPoint]);

            if(attack == false)
            {
                if (_distance < nextWayPointDistance)
                {
                    currentWayPoint++;
                }
            }


            _nextAttackTime += Time.deltaTime;
            if (_distanceToTarget < AttackDistance && _nextAttackTime >= AttackRate)
            {
                attack = true;
                Attack(UnityEngine.Random.Range(Damage - 5, Damage + 5));
                _nextAttackTime = 0;
            }
            else
            {
                attack = false;
                CheckDistance();
            }

            if(_enemyRigidbody.velocity.x > 0.01f)
            {
                transform.localScale = new Vector3(-_scale.x, _scale.y, _scale.z);
            }
            else if(_enemyRigidbody.velocity.x < -0.01f)
            {
                transform.localScale = new Vector3(_scale.x, _scale.y, _scale.z);
            }
        }

        private void UpdateAnimtion()
        {
            EnemyAnimator.SetFloat("xVelocity", _enemyRigidbody.velocity.x);
        }

        private void CheckDistance()
        {
            _distanceToTarget = Vector2.Distance(Target.position, _enemyRigidbody.position);
            
        }


        public void Attack(int damage)
        {
            EnemyAnimator.SetTrigger("Attack");

            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, PlayerLayer);

            foreach (Collider2D detectedEnemy in hitEnemy)
            {
                detectedEnemy.TryGetComponent<ITakeDamage>(out ITakeDamage takeDamage);
                
                takeDamage?.TakeDamage(damage);
            }

        }
        private void OnDrawGizmosSelected()
        {
            if (AttackPoint == null)
                return;

            Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
        }

    }

}


